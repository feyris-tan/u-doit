﻿//Generated by the u-doit class generator, written by feyris-tan.
using u_doit.I_DoIt;
using System;
using u_doit.I_DoIt.Objects;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;

namespace u_doit.Objects.Categories
{
	[CategoryId("C__CATS__PDU_BRANCH")]
	[CategoryDisplayName("Branch")]
	class Branch : Category
	{
		public int pdu_id;
		public int branch_id;
		public int receptables;
		public string description;
	}
}