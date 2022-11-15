using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcdsApp.Data
{
    public class Utility : ControllerBase
    {
        private readonly DataContext _context;
        public Utility(DataContext context)
        {
            _context = context;
        }

        public bool DoesHavePermissionToAddData(string roleId, string actionName)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(actionName))
                return false;

            var queryResult = _context.RoleWisePermittedContents
                .Include(r => r.UserPermittedContent)
                .FirstOrDefault(r => r.UserRoleId == roleId && r.UserPermittedContent.ActionName == actionName);

            return queryResult != null;
        }

        public bool DoesHavePermissionToEditData(string roleId, string actionName)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(actionName))
                return false;

            var queryResult = _context.RoleWisePermittedContents
                .Include(r => r.UserPermittedContent)
                .FirstOrDefault(r => r.UserRoleId == roleId && r.UserPermittedContent.ActionName == actionName);

            return queryResult != null;
        }
        
        public bool DoesHavePermissionToDeleteData(string roleId, string actionName)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(actionName))
                return false;

            var queryResult = _context.RoleWisePermittedContents
                .Include(r => r.UserPermittedContent)
                .FirstOrDefault(r => r.UserRoleId == roleId && r.UserPermittedContent.ActionName == actionName);

            return queryResult != null;
        }

    }
}
