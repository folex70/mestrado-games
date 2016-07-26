using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Temas  {

		public int IdTema{get;set;} 
		public string Tema{get;set;} 

		public Temas(int idtema,string tema)
		{

			this.IdTema       = idtema;
			this.Tema 	 	  = tema;
		}


}
