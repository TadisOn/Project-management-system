 
 
INFORMATIKOS FAKULTETAS 
 
T120B165 Saityno taikom?j? program? projektavimas 
Projekto �Darbuotoj? valdymo sistema� ataskaita 
 
 
 
	 	Studentas: 	Tadas Jutkus, IFF - 0/6 
	D?stytojai: 	Tomas Bla�auskas 
 
 
 
KAUNAS 2023 


Turinys 
1. Sprend�iamo u�davinio apra�ymas	3
1.1. Sistemos paskirtis	3
1.2. Funkciniai reikalavimai	3
2. Sistemos architekt?ra	4
 

1. Sprend�iamo u�davinio apra�ymas 
1.1. Sistemos paskirtis 
   Projekto tikslas � palengvinti darbdavio u�duo?i? priskyrim? ir j? atlikimo sekim? darbuotojams.
   Darbdavys gal?s sistemoje sukurti darbuotoj? paskyras, jiems suteikti prisijungimus prie sistemos. Darbuotojai prisijung? gal?s matyti kokius projektus jie turi ir kokias u�duotis tuose projektuose reik?s atlikti.
1.2. Funkciniai reikalavimai 
Neregistruotas sistemos naudotojas gal?s: 
1. Per�i?r?ti pagrindin? puslap? (Landing page).
2. Prisijungti kaip darbuotojas/darbdavys.
Darbuotojas gal?s: 
1. Atsijungti nuo sistemos.
2. Matyti priskirtus projektus.
3. Matyti projekte esamas u�duotis
4. Keisti u�duo?i? b?sen?.
 Darbdavys gal?s:
1. Sukurti darbuotojo paskyr?.
2. I�trinti darbuotojo paskyr?.
3. Redaguoti darbuotojo paskyr?.
4. Sukurti projekt?.
5. Redaguoti projekt?.
6. Priskirti projekt? darbuotojui.
7. I�trinti projekt?.
8. Sukurti u�duot? projekte.
9. I�trinti u�duot?.
10. Redaguoti u�duot?.
11. Matyti darbuotoj? s?ra�?.

2. Sistemos architekt?ra 
Sistemos sudedamosios dalys: 
� Kliento pus? (ang. Front-End) � naudojant React.js; 
� Serverio pus? (angl. Back-End) � naudojant C# ASP .NET Core, Duomen? baz? - MSSQL. 
2.1 pav. pavaizduota sistemos diegimo diagrama. Naudotojas, pasileid?s nar�ykl?, gal?s HTTPS protokolu pasiekti sistem?, patalpint? Azure serveryje. Sistemos veikimui naudojamas API, kuris komunikuoja su duomen? baze per ORM (Entity Framework).

	2.1 pav. sistemos diegimo diagrama 	 
 	 

 
 

2 
 



