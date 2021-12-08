using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models.ThemeModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace EcdsApp.Controllers.ThemeLayer
{
    public class ThemeLayerDetailsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ThemeLayerDetailsController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: ThemeLayerDetails
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ThemeLayerDetails.Include(t => t.SubThemes).Include(t => t.ThemeLayerTypes);
            return View(await dataContext.ToListAsync());
        }

        // GET: ThemeLayerDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(t => t.SubThemes)
                .Include(t => t.ThemeLayerTypes)
                .FirstOrDefaultAsync(m => m.LayerId == id);
            if (themeLayerDetail == null)
            {
                return NotFound();
            }

            return View(themeLayerDetail);
        }

        // GET: ThemeLayerDetails/Create
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName");

            return View();
        }

        // POST: ThemeLayerDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LayerId,SubThemeId,LayerPath,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName,FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName,IsLegendColor,LegendColorFieldName")] 
            ThemeLayerDetail themeLayerDetail, List<IFormFile> postedFile)
        {
            if (ModelState.IsValid && postedFile.Count > 0)
            {
                var newThemeLayerDetId = (_context.ThemeLayerDetails.Max(s => (int?)s.LayerId) ?? 0) + 1;
                themeLayerDetail.LayerId = newThemeLayerDetId;

                foreach (var orgFileName in postedFile.Select(file => ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value))
                {
                    themeLayerDetail.LayerFileName = orgFileName;
                }
                
                _context.Add(themeLayerDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);

            return View(themeLayerDetail);
        }

        // GET: ThemeLayerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails.FindAsync(id);
            if (themeLayerDetail == null)
            {
                return NotFound();
            }
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LayerId,SubThemeId,LayerPath,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName,FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName,IsLegendColor,LegendColorFieldName")] ThemeLayerDetail themeLayerDetail)
        {
            if (id != themeLayerDetail.LayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeLayerDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeLayerDetailExists(themeLayerDetail.LayerId))
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
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            return View(themeLayerDetail);
        }

        // GET: ThemeLayerDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(t => t.SubThemes)
                .Include(t => t.ThemeLayerTypes)
                .FirstOrDefaultAsync(m => m.LayerId == id);
            if (themeLayerDetail == null)
            {
                return NotFound();
            }

            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeLayerDetail = await _context.ThemeLayerDetails.FindAsync(id);
            _context.ThemeLayerDetails.Remove(themeLayerDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeLayerDetailExists(int id)
        {
            return _context.ThemeLayerDetails.Any(e => e.LayerId == id);
        }
    }
}
