﻿using SmartGymWomenRetrival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymWomenRetrival.Services
{
    public class DbService
    {
        public List<Customer> GetAllWomenCustomers()
        {
            smartgymEntities db = new smartgymEntities();
            return db.Customers.ToList();
        }


        public List<T_session_subscriber> GetAllWomenT_session_subscriber()
        {
            smartgymEntities db = new smartgymEntities();
            return db.T_session_subscriber.ToList();
        }

        public List<Membership> GetAllWomenMembership()
        {
            smartgymEntities db = new smartgymEntities();
            return db.Memberships.ToList();
        }
    }
}