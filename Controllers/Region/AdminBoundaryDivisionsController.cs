using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models;
using MySql.Data.MySqlClient;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EcdsApp.Controllers.Region
{
    public class AdminBoundaryDivisionsController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryDivisionsController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryDivisions
        public async Task<IActionResult> Index()
        {
            var columns = _context.GetColumns<DataContext>("AdminBoundaryDivisions"); //GetColumns<DataContext>("SomeProperty");
            //var columns = DataContext.GetColumn("AdminBoundaryDivisions"); //GetColumns<DataContext>("SomeProperty");
            var modelName = "AdminBoundaryDivisions";
            var columnName = "DivisionGeoCode,DivisionName";

            var myDictionary1 = new Dictionary<string, Func<DbContext, IQueryable>>
            {
                { "AdminBoundaryDivisions", ( DbContext context ) => context.Set<AdminBoundaryDivision>() }
            };

            var dbSet = myDictionary1[modelName].Invoke(_context);
            dbSet.ToDynamicList();

            var myDictionary = new Dictionary<string, Type>
            {
                {"AdminBoundaryDivisions", typeof(AdminBoundaryDivision)}
            };

            //DBContext dbContext = new DBContext();
            //var dbSet = _context.Set(myDictionary[modelName]);
            var entity = _context.Find(myDictionary[modelName], "10");

            var result = _context.Query(modelName).ToDynamicListAsync();
            //var query = _context.Set(modelName);
            //var result = query.ToList();

            //var data = _context.AdminBoundaryDivisions.FromSqlRaw("SELECT * FROM lkp_admin_boundary_divisions").ToList();
            var data = GetData("div_name", "lkp_admin_boundary_divisions");
            var jsonData = JsonSerializer.Serialize(data.Result);
            //var data = _context.ExecSql<AdminBoundaryDivision>("SELECT * FROM lkp_admin_boundary_divisions");


            return View(await _context.AdminBoundaryDivisions.ToListAsync());
        }

        private async Task<List<string>> GetData(string fieldName, string tableName)
        {
            try
            {
                var fields = new List<string>();

                var conn = new MySqlConnection("server=202.53.173.179;userid=drip_admin;pwd=#UndP^drIp@2020;database=ecds_db;Allow User Variables=True;");
                await conn.OpenAsync();
                var cmd = new MySqlCommand("Select " + fieldName + " from " + tableName + " ", conn);

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var field = reader[fieldName].ToString();
                    if (!string.IsNullOrEmpty(field))
                    {
                        fields.Add(field);
                    }
                }

                await conn.CloseAsync();
                return fields;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return new List<string>();
        }


        public async Task<IActionResult> SummaryData(string adminCode, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.DivisionCode = adminCode;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDivisions.Where(i=>i.DivisionGeoCode==adminCode).ToListAsync());
        }

        public async Task<IActionResult> DetailsData(int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDivisions.ToListAsync());
        }



        // GET: AdminBoundaryDivisions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions
                .FirstOrDefaultAsync(m => m.DivisionGeoCode == id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBoundaryDivisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DivisionGeoCode,DivisionName,DivisionNameBangla,SortingOrder")] AdminBoundaryDivision adminBoundaryDivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminBoundaryDivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions.FindAsync(id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }
            return View(adminBoundaryDivision);
        }

        // POST: AdminBoundaryDivisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DivisionGeoCode,DivisionName,DivisionNameBangla,SortingOrder")] AdminBoundaryDivision adminBoundaryDivision)
        {
            if (id != adminBoundaryDivision.DivisionGeoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminBoundaryDivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryDivisionExists(adminBoundaryDivision.DivisionGeoCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions
                .FirstOrDefaultAsync(m => m.DivisionGeoCode == id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDivision);
        }

        // POST: AdminBoundaryDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminBoundaryDivision = await _context.AdminBoundaryDivisions.FindAsync(id);
            _context.AdminBoundaryDivisions.Remove(adminBoundaryDivision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminBoundaryDivisionExists(string id)
        {
            return _context.AdminBoundaryDivisions.Any(e => e.DivisionGeoCode == id);
        }
    }
}
