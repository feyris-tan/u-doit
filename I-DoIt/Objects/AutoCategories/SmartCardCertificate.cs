﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATG__SMARTCARD_CERTIFICATE")]
	[CategoryDisplayName("Smart Card Certificate")]
	class SmartCardCertificate : Category
	{
		public string cardnumber;
		public string barring_password;
		public string pin_nr;
		public string reference;
		public DateTime expires_on;
		public string description;
	}
}