![](L0\_Ataskaita\_Tadas\_Jutkus.001.jpeg)INFORMATIKOS FAKULTETAS

**T120B165 Saityno taikomųjų programų projektavimas**

**Projekto „Darbuotojų valdymo sistema“ ataskaita**

Studentas: Tadas Jutkus, IFF - 0/6

Dėstytojai: Tomas Blažauskas

KAUNAS 2023





![](L0\_Ataskaita\_Tadas\_Jutkus.002.jpeg)**Turinys**

1. ` `**Sprendžiamo uždavinio aprašymas..................................................................................... 3**
1. ` `**Sistemos paskirtis......................................................................................................... 3**
1. ` `**Funkciniai reikalavimai...............................................................................................3**
1. ` `**Sistemos architektūra............................................................................................................4**
1. ` `**Sistemos struktūra..............................................................................................................4**
1. ` `**API specifikacija..................................................................................................................5**
1. ` `**Sistemos naudotojo sąsaja..............................................................................................14**

2





1. ![](L0\_Ataskaita\_Tadas\_Jutkus.002.jpeg) **Sprendžiamo uždavinio aprašymas**
1. ` `**Sistemos paskirtis**

Projekto tikslas – palengvinti darbdavio užduočių priskyrimą ir jų atlikimo sekimą

darbuotojams.

Darbdavys galės sistemoje sukurti darbuotojų paskyras, jiems suteikti prisijungimus prie

sistemos. Darbuotojai prisijungę galės matyti kokius projektus jie turi ir kokias užduotis tuose

projektuose reikės atlikti.

1. ` `**Funkciniai reikalavimai**

Neregistruotas sistemos naudotojas galės:

1. ` `Peržiūrėti pagrindinį puslapį (Landing page).
1. ` `Prisijungti kaip darbuotojas/darbdavys.

Darbuotojas galės:

1. ` `Atsijungti nuo sistemos.
1. ` `Matyti priskirtus projektus.
1. ` `Matyti projekte esamas užduotis
1. ` `Keisti užduočių būseną.

Darbdavys galės:

1. ` `Sukurti darbuotojo paskyrą.
1. ` `Ištrinti darbuotojo paskyrą.
1. ` `Redaguoti darbuotojo paskyrą.
1. ` `Sukurti projektą.
1. ` `Redaguoti projektą.
1. ` `Priskirti projektą darbuotojui.
1. ` `Ištrinti projektą.
1. ` `Sukurti užduotį projekte.
1. ` `Ištrinti užduotį.
1. ` `Redaguoti užduotį.
1. ` `Matyti darbuotojų sąrašą.

3





2. ![](L0\_Ataskaita\_Tadas\_Jutkus.003.jpeg) **Sistemos architektūra**

Sistemos sudedamosios dalys:

• Kliento pusė (ang. Front-End) – naudojant React.js;

• Serverio pusė (angl. Back-End) – naudojant C# ASP .NET Core, Duomenų bazė -

PostgreSQL.

1. ` `pav. pavaizduota sistemos diegimo diagrama. Naudotojas, pasileidęs naršyklę, galės

HTTPS protokolu pasiekti sistemą, patalpintą Azure serveryje. Sistemos veikimui

naudojamas API, kuris komunikuoja su duomenų baze per ORM (Entity Framework).

1. ` `**pav.** sistemos diegimo diagrama
2. ` `**Sistemos struktūra**

Serverio pusė sudaryta iš šių kontrolerių:

● ProjectEndpoints.cs - atsakingas už projektų CRUD ir kitas operacijas
● TaskEndpoints.cs - atsakingas už užduočių priklausančių projektui CRUD operacijas.
● WorkerEndpoints.cs - atsakingas už darbuotojų priklausančių užduočiai CRUD
 operacijas.

4





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





2. ![](L0\_Ataskaita\_Tadas\_Jutkus.013.jpeg) **Sistemos naudotojo sąsaja**

pav 1. Neprisijungusio vartotojo langas

pav 2. Prisijungimo langas

14





![](L0\_Ataskaita\_Tadas\_Jutkus.014.jpeg)pav 3. Pagrindinis langas prisijungus

pav 4. Projektų sąrašo langas

15





![](L0\_Ataskaita\_Tadas\_Jutkus.015.jpeg)pav 5. Projektų kūrimo langas

pav 6. Projekto ir užduočių langas

16





![](L0\_Ataskaita\_Tadas\_Jutkus.016.jpeg)pav 7. Projekto redagavimo langas

pav 8. Užduoties ir priskirtų darbuotojų langas

17





![](L0\_Ataskaita\_Tadas\_Jutkus.017.jpeg)pav 9. Darbuotojų priskyrimo langas

pav 10. Darbuotojų kūrimo langas

18
