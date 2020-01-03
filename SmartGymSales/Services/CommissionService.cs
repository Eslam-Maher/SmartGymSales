﻿using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity.Migrations;

namespace SmartGymSales.Services
{
    public class CommissionService
    {
        public List<commission> getAllCommissions(string user_name, string password) {

            var db = new SmartGymSalesEntities();
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();
            User currentUser = userService.GetUserbyUser_name(user_name);
            if (!userService.checkUserCred(user_name, password))
            {
                return null;
            }
            if (!userRolesService.isUserManger(user_name) && !userRolesService.isUserSales(user_name))
            {
                return null;
            }
            return  db.commissions.Where(x=>x.is_hidden==false).ToList();
        }

        internal List<String> insertCommission(string user_name, string password, commission commission)
        {
            var db = new SmartGymSalesEntities();
            UsersService userService = new UsersService();
            List<String> errors = new List<string>();
            UserRolesService userRolesService = new UserRolesService();
            User currentUser = userService.GetUserbyUser_name(user_name);
            if (!userService.checkUserCred(user_name, password))
            {
                return null;
            }
            if (!userRolesService.isUserManger(user_name) && !userRolesService.isUserSales(user_name))
            {
                return null;
            }
            bool CommissionError = false;

            if (db.commissions.Where(x=>x.is_hidden==false).Count() > 0) {
                CommissionError = true;
                errors.Add("can't add new commission before delete the existing one");
            }
            if (!(commission.new_customer_percentatge > 0 && commission.new_customer_percentatge < 100))
            {
                CommissionError = true;
                errors.Add("The percentage for each new member must be between 0 to 100 ");
            }
            if (!(commission.old_customer_percentatge > 0 && commission.old_customer_percentatge < 100))
            {
                CommissionError = true;
                errors.Add("The percentage for each old member must be between 0 to 100 ");
            }
            if (commission.target == 0) {
                CommissionError = true;
                errors.Add("The target can't be equal 0 ");
            }
            commission.is_hidden = false;
            commission.creation_date = DateTime.Now;
            if (!CommissionError)
            {
                db.commissions.Add(commission);
                db.SaveChanges();
            }
            return errors;
        }

        internal List<string> deleteCommission(string user_name, string password, int id)
        {
            var db = new SmartGymSalesEntities();
            UsersService userService = new UsersService();
            List<String> errors = new List<string>();
            UserRolesService userRolesService = new UserRolesService();
            User currentUser = userService.GetUserbyUser_name(user_name);
            if (!userService.checkUserCred(user_name, password))
            {
                return null;
            }
            if (!userRolesService.isUserManger(user_name) && !userRolesService.isUserSales(user_name))
            {
                return null;
            }
            bool CommissionError = false;
            commission comm = db.commissions.Find(id);
            if (comm == null) {
                errors.Add("Couldn't find commission in the database");
            }
            if (!CommissionError)
            {
                comm.is_hidden = true;
                db.commissions.AddOrUpdate(comm);
                db.SaveChanges();
            }
            return errors;

        }
    }
}