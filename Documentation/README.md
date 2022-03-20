# Slutliga uppgift/tenta

Deadline: 2021-10-15 kl 23:55

Detta är den slutliga uppgift/tenta för kursen Utveckling av molnbaserade applikationer.

Se kurs sidan för senaste information: [tenta](https://pgbsnh20.github.io/PGBSNH20-molnapplikationer/assignments/tenta) uppdateringar annonseras på PingPong och Teams

Vi har under [kursen](https://pgbsnh20.github.io/PGBSNH20-molnapplikationer/) gått igenom följande ämnen:
- Internet och moln
- Automatisering av bygg och release
- Containrar och orkestrering
- Serverless
- Databaser i molnet
- Webb applikationer i molnet
- Nätverk i molnet
- Filer i molnet
- Monitorering av moln applikationer
- Skalning, upp och ut
- Molnsäkerhetsfunktioner
- Moln-livscykel och miljöpåverkning



# Mini kalkylator
Bygg en mini-kalkylator (med .NET och C#) som kör i Azure, den ska bestå följande delar:
1)	[X] En databas: Cosmos DB (MongoDB)
2)	[X] En Azure Function som kan addera siffor
3)	[X] En Azure Function som kan subtrahera siffor
4)  [X] En App Service (Razor Page) applikation som tar in två siffor och en operator (+ eller -)
    * Webbgränssnitt (HTML + CSS)
    * Denna ska anropa rätt Azure Functions (beroende på om det är + eller -) som gör beräkningen
    * Denna ska visa svaret på beräkningen
    * Ska visa dom 10 senaste beräkningar från databasen

5)  [X] Manuell deploy till Azure
6)  [X] Logging till Azure Insights
7)  [X] GitHub Action bygg pipeline av koden (CI)
8)	[] Extra: Koden ska bygga och levereras som Docker images i GitHub
9)	[] Extra: Automatisk deploy (CD)
    * Med docker images eller deployat direkt
10) [X] Extra: Spara alla nyckeler och conntection strings i Azure Key Valut

## Målet

Målet med denna uppgift är att visa på en praktisk förståelse av dom ämnen som vi har varit igenom under kursen.

Dom flesta delar har du redan implementerat i din blogg (eller också har den som du gjorde kammeratgransking av, gjort det), så målet här är att visa att du kan sätta ihop allt till ett system och visa på en förståelse av det samlade system och hur dom olika delar påverkar varandra. 

## Betygskriterier

**G**:   ALLA delar 1 till 7 måste vara implementerat

**VG**:   Du måste uppfylla **G**-kriterierna, och visa en fördjupat kunskap i din inlämning, detta kan t.ex. vara genom en extra utförlig projekt beskrivning eller implementation av Extra punkterna (8 till 10).

# Leverans / Inlämning
Leverans/Inlämning ska ske i detta repository och ska innehålla:
* Koden till alla tjänster
* Ersätt denna README fil med en beskrivning av projekt (du får såklart gärna flytta innehållet till en annan fil)
    * Lägg till diagram vart det gir mening
    * Beskriv vilka delar som projektet består av och dom funkar ihop
* VIKTIGT: Lägg upp länk till detta repo i [PingPong](https://yh.pingpong.se/courseId/13405/content.do?id=6113928) när du är klar!

