using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Data
{
    public class Event
    {
		private int _id;
		private int? _tickets;
		private double? _price;
        private string _name, _type, _organizer, _description, _location;
		private DateTime? _date;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}		
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}
		public string Organizer
		{
			get { return _organizer; }
			set { _organizer = value; }
		}		
		public DateTime? Date
		{
			get { return _date; }
			set { _date = value; }
		}
		public string Location
		{
			get { return _location; }
			set { _location = value; }
		}
		public int? Tickets
		{
			get { return _tickets; }
			set { _tickets = value; }
		}
		public double? Price
		{
			get { return _price; }
			set { _price = value; }
		}
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public Event(string name, string type, string organizer)
		{
			Name = name;
			Type = type;
			Organizer = organizer;
		}

		//public Event(int id, string name, string type, string organizer, int? tickets)
		//	:this(name, type, organizer)
		//{
		//	Id = id;
		//	Tickets = tickets;
		//}

		public Event(string name, string type, string organizer, DateTime? date, string location, int? tickets, double? price, string description)
			: this(name, type, organizer)
		{
			Date = date;
			Location = location;			
			Tickets = tickets;
			Price = price;
			Description = description;
		}

		public Event(int id, string name, string type, string organizer, DateTime? date, string location, int? tickets, double? price, string description)
			: this(name, type, organizer, date, location, tickets, price, description)
		{
			Id = id;
		}
	}
}
