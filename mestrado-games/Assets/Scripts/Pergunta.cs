using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pergunta{
	
	public int IdPergunta{get;set;} 
	public string Pergunta1{get;set;} 
	public string Alternativa1{get;set;} 
	public string Alternativa2{get;set;} 
	public string Alternativa3{get;set;} 
	public string Alternativa4{get;set;} 
	public string Resposta1{get;set;}
	public int Pontos{get;set;}
	public int IdTema{get;set;}
	public string Tema{get;set;}
	//quiz
	public Pergunta(int idpergunta,
					string pergunta1,
					string alternativa1,
					string alternativa2,
					string alternativa3,
					string alternativa4,
					string resposta1,
					int pontos,			
					int idtema,
					string tema	)
	{
		this.IdPergunta = idpergunta;
		this.Pergunta1 = pergunta1;
		this.Alternativa1 = alternativa1;
		this.Alternativa2 = alternativa2;
		this.Alternativa3 = alternativa3;
		this.Alternativa4 = alternativa4;
		this.Resposta1 	  = resposta1;
		this.Pontos       = pontos;
		this.IdTema       = idtema;
		this.Tema 	 	  = tema;
	}
	//forca
	public Pergunta(int idpergunta,
		string pergunta1,
		string alternativa1,
		string alternativa2,
		string alternativa3,
		string alternativa4,
		int pontos,			
		int idtema,
		string tema	)
	{
		this.IdPergunta = idpergunta;
		this.Pergunta1 = pergunta1;
		this.Alternativa1 = alternativa1;
		this.Alternativa2 = alternativa2;
		this.Alternativa3 = alternativa3;
		this.Alternativa4 = alternativa4;
		this.Pontos       = pontos;
		this.IdTema       = idtema;
		this.Tema 	 	  = tema;
	}
	//vouf
	public Pergunta(int idpergunta,
					string pergunta1,
					string resposta1,
					int idtema,
					string tema	)
	{
		this.IdPergunta 	= idpergunta;
		this.Pergunta1 		= pergunta1;
		this.Resposta1 	  	= resposta1;
		this.IdTema       	= idtema;
		this.Tema 	 	  	= tema;
	}
}