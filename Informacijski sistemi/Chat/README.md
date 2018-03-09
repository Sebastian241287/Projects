**ServiceChat**

*Avtorji:*

EFTIM EFTIMOV - 63150346

SEBASTIAN ALEJANDRO MONTANEZ - 63100316

![android1.png](https://bitbucket.org/repo/5K8XBj/images/993494138-android1.png)
![android2.png](https://bitbucket.org/repo/5K8XBj/images/2059766050-android2.png)

![adminscreen.jpg](https://bitbucket.org/repo/5K8XBj/images/2852172784-adminscreen.jpg)

**Opis delovanja/težav pri izdelavi aplikacije/izboljšav**
Delovanje ServiceCHAT je podobno kot delovanje prejšnje naloge (ChatDB). Dodanih pa je bilo več aktivnosti. 
Uporabniki, ki imajo administratorske pravice, lahko dostopajo do administratorskega spletnega vmesnika. V njem se mu izpišejo vsi obstoječi  uporabniki in podatek, koliko sporočil je vsak od njih napisal. Uporabnik, ki ima dostop do tega vmesnika, lahko doda administratorske pravice drugim uporabnikom, prav tako pa lahko tudi izbriše druge uporabnike in vsa njihova sporočila.
Druga aktivnost, ki je bila dodana, je dostop do aplikacije preko mobilnega aparata. Katerikoli uporabnik, ki se je prej registriral preko računalnika v ta chat, se lahko logira in uporablja aplikacijo preko mobilnega telefona ali tablice, ki uporablja operacijski sistem Android. Pri tem je možno dostopati do chat-a in pošiljati sporočila. Registracija in urejanje administratorske pravice pa je možna samo preko uporabe računalnika.
Težava se je pojavila pri funkciji »Send« v WCF servisu. Čeprav sporočila pošljamo preko http POST metode, sva želela, da tega ne bi naredili preko URL-ja, a nama žal tudi po dolgem iskanju informacije na internetu to ni uspelo.


**Opis nalog, ki jih je izvedel vsak izmed študentov**
Nalogo sva si razdelila tako, da sem Sebastian Alejandro Montañez spremenil celo drugo nalogo tako, da bi delovala z Azure app service, poleg tega pa sem programiral WFC servis in Administratorski vmesnik. Partner Eftim Eftimov pa je programiral cel del Androida te naloge, kar vključuje ustvarjanje  Activity-jev in povezavo z WFC servisom.

![db1.jpg](https://bitbucket.org/repo/5K8XBj/images/837192573-db1.jpg)
Podatkovno bazo sem spremenil tako, da je bil v tabeli pogovor dodan stolp »čas«, v katerem se zabeleži čas, ko je bilo sporočilo poslano.

![db2.jpg](https://bitbucket.org/repo/5K8XBj/images/4160758702-db2.jpg)
V tabeli uporabnik je bil dodan stolp »admin«, v katerem se shrani podatek, ali ima uporabnik administratorske pravice ali ne.