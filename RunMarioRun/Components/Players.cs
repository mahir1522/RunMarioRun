using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RunMarioRun.Components
{
	public class Players
	{
		public string Name { get; set; }
		public int Score {  get; set; }

		public static List<Players> AllPlayers { get; } = new List<Players>();

		public Players(string name, int score)
		{
			Name = name;
			Score = score;

			AllPlayers.Add(this);
		}
	}
}
