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
				Queue<Man> men = new Queue<Man> ();
				List<Person> women = new List<Person> ();
				int num = 0;
				int count = 0;

				string line = Console.ReadLine ();
				while (line != "0") {
					//Gives the number of pairs to make
					if (line.Length == 1) {
						if (count > 0) {
							Console.WriteLine ("Instance " + count + ":");
							int cnt = 1;
							while (cnt <= num) {
								for (int i = 0; i < num; i++) {
									if (women [i].Partner.name == cnt) {
										Console.WriteLine (women [i].Partner.Partner.name);
										cnt++;
									}
								}
							}
						}

						count++;


						num = Convert.ToInt32 (line);
						men.Clear();
						women.Clear();
						//Hello my name is Sheldon

						//Forms the Preference List for Men
						for (int i = 0; i < num; i++) {
							line = Console.ReadLine ();
							men.Enqueue (new Man (line, i+1));



						}
						//Forms the Preference List for Women
						for (int i = 0; i < num; i++) {
							line = Console.ReadLine ();
							women.Add (new Person (line, i+1));



						}
						int freemen = num;
						while (freemen != 0) {
							Man currentMan = men.Peek ();


							int choice = Convert.ToInt32 (currentMan.prefQueue.Peek ());
							if (women [choice - 1].Married == false) {

								currentMan.GetMarried (women [choice - 1]);
								currentMan.prefQueue.Dequeue ();
								men.Dequeue();
								freemen--;
							} else {
								if (women [choice - 1].Prefers (currentMan)) {
									Man test = (Man)women [choice - 1].GetMPartner ();
									men.Enqueue (test);
									women [choice - 1].GetDivorced ();
									currentMan.GetMarried (women [choice - 1]);
									currentMan.prefQueue.Dequeue ();
									men.Dequeue ();


								} else {
									currentMan.prefQueue.Dequeue ();
								}
							}

						}
					}





					line = Console.ReadLine();

				}
				Console.WriteLine ("Instance " + count + ":");
				int cnt2 = 1;

				while (cnt2 <= num) {
					for (int i = 0; i < num; i++) {
						if (women [i].Partner.name == cnt2) {
							Console.WriteLine (women [i].Partner.Partner.name);
							cnt2++;
						}
					}
				}
			}

		}
	}
}
