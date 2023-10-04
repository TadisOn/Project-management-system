 
 
INFORMATIKOS FAKULTETAS 
 
T120B165 Saityno taikom?j? program? projektavimas 
Projekto „Darbuotoj? valdymo sistema“ ataskaita 
 
 
 
	 	Studentas: 	Tadas Jutkus, IFF - 0/6 
	D?stytojai: 	Tomas Blažauskas 
 
 
 
KAUNAS 2023 


Turinys 
1. Sprendžiamo uždavinio aprašymas	3
1.1. Sistemos paskirtis	3
1.2. Funkciniai reikalavimai	3
2. Sistemos architekt?ra	4
 

1. Sprendžiamo uždavinio aprašymas 
1.1. Sistemos paskirtis 
   Projekto tikslas – palengvinti darbdavio užduo?i? priskyrim? ir j? atlikimo sekim? darbuotojams.
   Darbdavys gal?s sistemoje sukurti darbuotoj? paskyras, jiems suteikti prisijungimus prie sistemos. Darbuotojai prisijung? gal?s matyti kokius projektus jie turi ir kokias užduotis tuose projektuose reik?s atlikti.
1.2. Funkciniai reikalavimai 
Neregistruotas sistemos naudotojas gal?s: 
1. Perži?r?ti pagrindin? puslap? (Landing page).
2. Prisijungti kaip darbuotojas/darbdavys.
Darbuotojas gal?s: 
1. Atsijungti nuo sistemos.
2. Matyti priskirtus projektus.
3. Matyti projekte esamas užduotis
4. Keisti užduo?i? b?sen?.
 Darbdavys gal?s:
1. Sukurti darbuotojo paskyr?.
2. Ištrinti darbuotojo paskyr?.
3. Redaguoti darbuotojo paskyr?.
4. Sukurti projekt?.
5. Redaguoti projekt?.
6. Priskirti projekt? darbuotojui.
7. Ištrinti projekt?.
8. Sukurti užduot? projekte.
9. Ištrinti užduot?.
10. Redaguoti užduot?.
11. Matyti darbuotoj? s?raš?.

2. Sistemos architekt?ra 
Sistemos sudedamosios dalys: 
• Kliento pus? (ang. Front-End) – naudojant React.js; 
• Serverio pus? (angl. Back-End) – naudojant C# ASP .NET Core, Duomen? baz? - MSSQL. 
2.1 pav. pavaizduota sistemos diegimo diagrama. Naudotojas, pasileid?s naršykl?, gal?s HTTPS protokolu pasiekti sistem?, patalpint? Azure serveryje. Sistemos veikimui naudojamas API, kuris komunikuoja su duomen? baze per ORM (Entity Framework).

	2.1 pav. sistemos diegimo diagrama 	 
 	 

 
 

2 
 



