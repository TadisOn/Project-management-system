<!-----

You have some errors, warnings, or alerts. If you are using reckless mode, turn it off to see inline alerts.
* ERRORs: 0
* WARNINGs: 0
* ALERTS: 12

Conversion time: 4.314 seconds.


Using this Markdown file:

1. Paste this output into your source file.
2. See the notes and action items below regarding this conversion run.
3. Check the rendered output (headings, lists, code blocks, tables) for proper
   formatting and use a linkchecker before you publish this page.

Conversion notes:

* Docs to Markdown version 1.0β35
* Tue Jan 02 2024 02:54:58 GMT-0800 (PST)
* Source doc: L0_Ataskaita_Tadas_Jutkus
* Tables are currently converted to HTML tables.
* This document has images: check for >>>>>  gd2md-html alert:  inline image link in generated source and store images to your server. NOTE: Images in exported zip file from Google Docs may not appear in  the same order as they do in your doc. Please check the images!


WARNING:
You have 4 H1 headings. You may want to use the "H1 -> H2" option to demote all headings by one level.

----->


<p style="color: red; font-weight: bold">>>>>>  gd2md-html alert:  ERRORs: 0; WARNINGs: 1; ALERTS: 12.</p>
<ul style="color: red; font-weight: bold"><li>See top comment block for details on ERRORs and WARNINGs. <li>In the converted Markdown or HTML, search for inline alerts that start with >>>>>  gd2md-html alert:  for specific instances that need correction.</ul>

<p style="color: red; font-weight: bold">Links to alert messages:</p><a href="#gdcalert1">alert1</a>
<a href="#gdcalert2">alert2</a>
<a href="#gdcalert3">alert3</a>
<a href="#gdcalert4">alert4</a>
<a href="#gdcalert5">alert5</a>
<a href="#gdcalert6">alert6</a>
<a href="#gdcalert7">alert7</a>
<a href="#gdcalert8">alert8</a>
<a href="#gdcalert9">alert9</a>
<a href="#gdcalert10">alert10</a>
<a href="#gdcalert11">alert11</a>
<a href="#gdcalert12">alert12</a>

<p style="color: red; font-weight: bold">>>>>> PLEASE check and correct alert issues and delete this message and the inline alerts.<hr></p>



    

<p id="gdcalert1" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image1.jpg). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert2">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image1.jpg "image_tooltip")
 


     

INFORMATIKOS FAKULTETAS 


     


    **T120B165 Saityno taikomųjų programų projektavimas **

**Projekto „Darbuotojų valdymo sistema“ ataskaita **


    ** **


    ** **


    ** **

	 	Studentas: 	Tadas Jutkus, IFF - 0/6 

	Dėstytojai: 	Tomas Blažauskas 

<p style="text-align: right">
 </p>



     


     

KAUNAS 2023 


**Turinys **


[TOC]


 



1. Sprendžiamo uždavinio aprašymas 
    1. Sistemos paskirtis 

Projekto tikslas – palengvinti darbdavio užduočių priskyrimą ir jų atlikimo sekimą darbuotojams.

Darbdavys galės sistemoje sukurti darbuotojų paskyras, jiems suteikti prisijungimus prie sistemos. Darbuotojai prisijungę galės matyti kokius projektus jie turi ir kokias užduotis tuose projektuose reikės atlikti.



    2. Funkciniai reikalavimai 

Neregistruotas sistemos naudotojas galės: 



1. Peržiūrėti pagrindinį puslapį (Landing page).
2. Prisijungti kaip darbuotojas/darbdavys.

    Darbuotojas galės: 

1. Atsijungti nuo sistemos.
2. Matyti priskirtus projektus.
3. Matyti projekte esamas užduotis
4. Keisti užduočių būseną.

 Darbdavys galės:



1. Sukurti darbuotojo paskyrą.
2. Ištrinti darbuotojo paskyrą.
3. Redaguoti darbuotojo paskyrą.
4. Sukurti projektą.
5. Redaguoti projektą.
6. Priskirti projektą darbuotojui.
7. Ištrinti projektą.
8. Sukurti užduotį projekte.
9. Ištrinti užduotį.
10. Redaguoti užduotį.
11. Matyti darbuotojų sąrašą.
2. Sistemos architektūra 

Sistemos sudedamosios dalys: 



* Kliento pusė (ang. Front-End) – naudojant React.js; 
* Serverio pusė (angl. Back-End) – naudojant C# ASP .NET Core, Duomenų bazė - PostgreSQL. 

2.1 pav. pavaizduota sistemos diegimo diagrama. Naudotojas, pasileidęs naršyklę, galės HTTPS protokolu pasiekti sistemą, patalpintą Azure serveryje. Sistemos veikimui naudojamas API, kuris komunikuoja su duomenų baze per ORM (Entity Framework).

<p style="text-align: right">


<p id="gdcalert2" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image2.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert3">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


<img src="images/image2.png" width="" alt="alt_text" title="image_tooltip">
</p>


	**2.1 pav.** sistemos diegimo diagrama_ 	 _


#  	3. Sistemos struktūra

Serverio pusė sudaryta iš šių kontrolerių:



* ProjectEndpoints.cs - atsakingas už projektų CRUD ir kitas operacijas
* TaskEndpoints.cs - atsakingas už užduočių priklausančių projektui CRUD operacijas.
* WorkerEndpoints.cs - atsakingas už darbuotojų priklausančių užduočiai CRUD operacijas.




# 	4. API specifikacija


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetProjects
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti visų projektų sąrašą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“projectId”: “...”,
<p>
“name”: “...”,
<p>
“description”:”...”
<p>
},
<p>
{
<p>
“projectId”:”...”,
<p>
“name”:”...”,
<p>
…
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
   </td>
  </tr>
</table>


lentelė 1. Projektų gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetProject
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti vieno projektą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectID}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“projectId”: “...”,
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 2. Projekto gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>CreateProject
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Sukurti projektą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“projectId”: “...”,
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Created(201)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
   </td>
  </tr>
</table>


lentelė 3. Projektų kūrimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>UpdateProject
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Redaguoti sukurtą projektą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 4. Projektų redagavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>DeleteProject
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Ištrinti projektą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 5. Projektų trynimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetTasks
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti visų užduočių sąrašą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“taskId”: “...”,
<p>
“name”: “...”,
<p>
“description”:”...”
<p>
},
<p>
{
<p>
“taskId”:”...”,
<p>
“name”:”...”,
<p>
…
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
   </td>
  </tr>
</table>


lentelė 6. Užduočių gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetTask
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti vieno užduotį
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectID}/tasks/{taskId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“taskId”: “...”,
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 7. Užduoties gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>CreateTask
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Sukurti užduotį
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“taskId”: “...”,
<p>
“name”:”...”,
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Created(201)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
   </td>
  </tr>
</table>


lentelė 8. Užduoties kūrimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>UpdateTask
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Redaguoti sukurtą projektą
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“description”:”...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 9. Užduoties redagavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>DeleteTask
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Ištrinti užduotį
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 10. Užduoties trynimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetWrokers
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti visų darbuotojų sąrašas
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}/workers
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“workerId”: “...”,
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
“Username”: “...”,
<p>
“CreationDate”: “...”,
<p>
},
<p>
{
<p>
“workerId”: “...”,
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
…
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
   </td>
  </tr>
</table>


lentelė 11. Darbuotojų gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>GetWorker
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Gauti vieną darbuotoją
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}/workers/{workerId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“workerId”: “...”,
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
“Username”: “...”,
<p>
“CreationDate”: “...”,
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 12. Darbuotojo gavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>CreateWorker
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Sukurti darbuotoją
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}/workers/{workerId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
“Username”: “...”,
<p>
“Email”:”...”,
<p>
“Password”:””...
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>[{
<p>
“workerId”: “...”,
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
“Username”: “...”,
<p>
“CreationDate”: “...”,
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Created(201)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
   </td>
  </tr>
</table>


lentelė 13. Darbuotojo kūrimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>UpdateWorker
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Redaguoti sukurtą darbuotoją
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}/workers/{workerId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>[{
<p>
“Firstname”: “...”,
<p>
“Lastname”: “...”,
<p>
“Username”: “...”
<p>
}]
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
422- jei neatitinka validacijos.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 14. Darbuotojo redagavimo API


<table>
  <tr>
   <td>API metodas
   </td>
   <td>DeleteWorker
   </td>
  </tr>
  <tr>
   <td>Paskirtis
   </td>
   <td>Ištrinti darbuotoją
   </td>
  </tr>
  <tr>
   <td>Kelias iki metodo
   </td>
   <td>api/projects/{projectId}/tasks/{taskId}/workers/{workerId}
   </td>
  </tr>
  <tr>
   <td>Užklausos struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Header dalis
   </td>
   <td>Authorization: Bearer {token}
   </td>
  </tr>
  <tr>
   <td>Atsakymo struktūra
   </td>
   <td>-
   </td>
  </tr>
  <tr>
   <td>Atsakymo kodas
   </td>
   <td>Ok(200)
   </td>
  </tr>
  <tr>
   <td>Galimi klaidų kodai
   </td>
   <td>401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.
<p>
404 - jei nerastas.
   </td>
  </tr>
</table>


lentelė 15. Darbuotojo trynimo API


# 


# 		5. Sistemos naudotojo sąsaja



<p id="gdcalert3" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image3.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert4">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image3.png "image_tooltip")


pav 1. Neprisijungusio vartotojo langas



<p id="gdcalert4" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image4.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert5">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image4.png "image_tooltip")


pav 2. Prisijungimo langas



<p id="gdcalert5" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image5.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert6">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image5.png "image_tooltip")


pav 3. Pagrindinis langas prisijungus



<p id="gdcalert6" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image6.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert7">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image6.png "image_tooltip")


pav 4. Projektų sąrašo langas



<p id="gdcalert7" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image7.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert8">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image7.png "image_tooltip")


pav 5. Projektų kūrimo langas



<p id="gdcalert8" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image8.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert9">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image8.png "image_tooltip")


pav 6. Projekto ir užduočių langas



<p id="gdcalert9" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image9.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert10">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image9.png "image_tooltip")


pav 7. Projekto redagavimo langas



<p id="gdcalert10" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image10.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert11">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image10.png "image_tooltip")


pav 8. Užduoties ir priskirtų darbuotojų langas



<p id="gdcalert11" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image11.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert12">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image11.png "image_tooltip")


pav 9. Darbuotojų priskyrimo langas



<p id="gdcalert12" ><span style="color: red; font-weight: bold">>>>>>  gd2md-html alert: inline image link here (to images/image12.png). Store image on your image server and adjust path/filename/extension if necessary. </span><br>(<a href="#">Back to top</a>)(<a href="#gdcalert13">Next alert</a>)<br><span style="color: red; font-weight: bold">>>>>> </span></p>


![alt_text](images/image12.png "image_tooltip")


pav 10. Darbuotojų kūrimo langas
