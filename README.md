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

● ProjectEndpoints.cs - atsakingas už projektų CRUD ir kitas operacijas● TaskEndpoints.cs - atsakingas už užduočių priklausančių projektui CRUD operacijas.● WorkerEndpoints.cs - atsakingas už darbuotojų priklausančių užduočiai CRUD operacijas.

4





2. ![](L0\_Ataskaita\_Tadas\_Jutkus.004.jpeg) **API specifikacija**

API metodas GetProjectsPaskirtis Gauti visų projektų sąrašąKelias iki metodo api/projectsUžklausos struktūra -

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{

“projectId”: “...”, “name”: “...”, “description”:”...” }, {

“projectId”:”...”, “name”:”...”,

…}]

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

lentelė 1. Projektų gavimo API

5





![](L0\_Ataskaita\_Tadas\_Jutkus.005.jpeg)API metodas GetProject

Paskirtis Gauti vieno projektąKelias iki metodo api/projects/{projectID}Užklausos struktūra -

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{

“projectId”: “...”,“name”:”...”,“description”:”...”}]

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 2. Projekto gavimo API

API metodas CreateProjectPaskirtis Sukurti projektąKelias iki metodo api/projects Užklausos struktūra [{

“name”:”...”,“description”:”...”}]

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{

“projectId”: “...”,“name”:”...”,“description”:”...”}]

Atsakymo kodas Created(201)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

422- jei neatitinka validacijos.

lentelė 3. Projektų kūrimo API

6





![](L0\_Ataskaita\_Tadas\_Jutkus.006.jpeg)API metodas UpdateProject

Paskirtis Redaguoti sukurtą projektąKelias iki metodo api/projects/{projectId} Užklausos struktūra [{

“description”:”...”}]

Header dalis Authorization: Bearer {token}Atsakymo struktūra - Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.422- jei neatitinka validacijos.404 - jei nerastas.

lentelė 4. Projektų redagavimo API

API metodas DeleteProjectPaskirtis Ištrinti projektąKelias iki metodo api/projects/{projectId}Užklausos struktūra -

Header dalis Authorization: Bearer {token}Atsakymo struktūra - Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 5. Projektų trynimo API

API metodas GetTasks

Paskirtis Gauti visų užduočių sąrašą

Kelias iki metodo api/projects/{projectId}/tasks

Užklausos struktūra -

7





![](L0\_Ataskaita\_Tadas\_Jutkus.007.jpeg)Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{

“taskId”: “...”, “name”: “...”, “description”:”...”

}, {

“taskId”:”...”, “name”:”...”,

…}]

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

lentelė 6. Užduočių gavimo API

API metodas GetTask

Paskirtis Gauti vieno užduotįKelias iki metodo api/projects/{projectID}/tasks/{taskId}Užklausos struktūra -

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{ “taskId”: “...”,

“name”:”...”, “description”:”...”

8





![](L0\_Ataskaita\_Tadas\_Jutkus.008.jpeg)}]

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 7. Užduoties gavimo API

API metodas CreateTask

Paskirtis Sukurti užduotį

Kelias iki metodo api/projects/{projectId}/tasks

Užklausos struktūra [{

“name”:”...”,“description”:”...”}]

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{

“taskId”: “...”,“name”:”...”,“description”:”...”}]

Atsakymo kodas Created(201)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

422- jei neatitinka validacijos.

lentelė 8. Užduoties kūrimo API

API metodas UpdateTask

Paskirtis Redaguoti sukurtą projektą

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}

Užklausos struktūra [{

“description”:”...”}]

Header dalis Authorization: Bearer {token}

9





![](L0\_Ataskaita\_Tadas\_Jutkus.009.jpeg)Atsakymo struktūra -

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.422- jei neatitinka validacijos.404 - jei nerastas.

lentelė 9. Užduoties redagavimo API

API metodas DeleteTaskPaskirtis Ištrinti užduotįKelias iki metodo api/projects/{projectId}/tasks/{taskId}Užklausos struktūra -

Header dalis Authorization: Bearer {token}Atsakymo struktūra - Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 10. Užduoties trynimo API

API metodas GetWrokers

Paskirtis Gauti visų darbuotojų sąrašas

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}/wor kers

Užklausos struktūra -

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{ “workerId”: “...”,

“Firstname”: “...”, “Lastname”: “...”, “Username”: “...”, “CreationDate”: “...”,

10





![](L0\_Ataskaita\_Tadas\_Jutkus.010.jpeg)}, {

“workerId”: “...”, “Firstname”: “...”, “Lastname”: “...”,

…}]

Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

lentelė 11. Darbuotojų gavimo API

API metodas GetWorker

Paskirtis Gauti vieną darbuotoją

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}/work ers/{workerId}

Užklausos struktūra -

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{ “workerId”: “...”,

“Firstname”: “...”, “Lastname”: “...”, “Username”: “...”, “CreationDate”: “...”,}]

11





![](L0\_Ataskaita\_Tadas\_Jutkus.011.jpeg)Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 12. Darbuotojo gavimo API

API metodas CreateWorkerPaskirtis Sukurti darbuotoją

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}/work ers/{workerId}

Užklausos struktūra [{

“Firstname”: “...”,“Lastname”: “...”,“Username”: “...”,“Email”:”...”,“Password”:””...}]

Header dalis Authorization: Bearer {token}

Atsakymo struktūra [{ “workerId”: “...”,

“Firstname”: “...”, “Lastname”: “...”, “Username”: “...”, “CreationDate”: “...”,}]

Atsakymo kodas Created(201)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT žetonas yra neteisingas.

422- jei neatitinka validacijos.

lentelė 13. Darbuotojo kūrimo API

API metodas UpdateWorker

Paskirtis Redaguoti sukurtą darbuotoją

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}/work ers/{workerId}

Užklausos struktūra [{

12





![](L0\_Ataskaita\_Tadas\_Jutkus.012.jpeg)“Firstname”: “...”,“Lastname”: “...”,“Username”: “...”}]

Header dalis Authorization: Bearer {token}Atsakymo struktūra - Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.422- jei neatitinka validacijos.404 - jei nerastas.

lentelė 14. Darbuotojo redagavimo API

API metodas DeleteWorkerPaskirtis Ištrinti darbuotoją

Kelias iki metodo api/projects/{projectId}/tasks/{taskId}/work ers/{workerId}

Užklausos struktūra -

Header dalis Authorization: Bearer {token}Atsakymo struktūra - Atsakymo kodas Ok(200)

Galimi klaidų kodai 401 – jei siunčiamas autorizacijos JWT

žetonas yra neteisingas.404 - jei nerastas.

lentelė 15. Darbuotojo trynimo API

13





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
