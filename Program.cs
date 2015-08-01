using System;
using System.Collections.Generic;

namespace GaleShapely
{
	class Person
	{
		public string[] Preference_List { get; set; }
		public Queue<string> prefQueue { get; set; }
		public Person Partner { get; set; }
		public bool Married;
		public int name;


		public string[] GetPref(){
			return Preference_List;
		}



		public Person(string Preferences, int nm){
			prefQueue = new Queue<string>();
			name = nm;
			Preference_List = Preferences.Split (' ');
			foreach (string element in Preference_List) {
				prefQueue.Enqueue (element);
			}


			Married = false;


		}

		public int GetName(){
			return(name);
		}

		public void GetMarried(Person Pt){
			this.Married = true;
			Pt.Married = true;

			this.Partner = Pt;
			Pt.Partner = this;

		}

		public void GetDivorced(){
			this.Married = false;
			Partner.Married = false;

			this.Partner = null;
			//this.Partner.Partner = null;
		}

		public Person GetMPartner(){
			return(this.Partner);
		}

		public int GetPartner(){
			return(this.Partner.name);
		}

		public bool Prefers(Person Suitor){
			foreach(string element in this.prefQueue){
				if(Convert.ToInt32(element) == Suitor.name){
					return true;
				} else if (Convert.ToInt32(element) == this.Partner.name){
					return false;
				}
			}
			return false;

		}

		class Man:Person {



			public Man(string Preferences, int nm)
				: base(Preferences, nm) { 



				Married = false;


			}
		}
		class MainClass
		{


			public static void Main (string[] args)
			{
				List<Man> men = new List<Man> ();
				List<Person> women = new List<Person> ();
				int num = 0;
				int count = 0;

				string line = Console.ReadLine ();
				while (line != "0") {
					//Gives the number of pairs to make
					if (line.Length == 1) {
						if (count > 0) {
							Console.WriteLine ("Instance " + count + ":");
							for (int i = 0; i < num; i++) {
								Console.WriteLine (men [i].Partner.name);
							}
						
						}
						count++;
						num = Convert.ToInt32 (line);
						men.Clear();
						women.Clear();

						//Forms the Preference List for Men
						for (int i = 0; i < num; i++) {
							line = Console.ReadLine ();
							men.Add (new Man (line, i+1));
						}
						//Forms the Preference List for Women
						for (int i = 0; i < num; i++) {
							line = Console.ReadLine ();
							women.Add (new Person (line, i+1));
						}
						Man currentMan = null;
						int freemen = num;
						while (freemen != 0) {
							for (int i = 0; i < num; i++) {
								if (men [i].Married == false) {
									currentMan = men [i];
									break;
								}
							}
							int choice = Convert.ToInt32 (currentMan.prefQueue.Dequeue());
							if (women [choice - 1].Married == false) {
								currentMan.GetMarried (women [choice - 1]);
								freemen--;
							} else {
								Console.WriteLine ("Conflict: " + currentMan.prefQueue.Peek());
								if (women [choice - 1].Prefers (currentMan)) {
									Console.WriteLine ("Prefers New Man");
									women [choice - 1].GetDivorced ();
									currentMan.GetMarried (women [choice - 1]);


								}
							}

						}
					}





					line = Console.ReadLine();

				}
				Console.WriteLine ("Instance " + count + ":");
					for (int i = 0; i < num; i++) {
						Console.WriteLine (men [i].Partner.name);
					}
			}

		}
	}
}
