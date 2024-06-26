Relazione Applicazione Web 
Esame di Applicazioni Web Mobile 
L'applicazione presentata ha come obiettivo la gestione efficiente di una vetrina online specializzata in articoli dedicati ad anelli e bracciali tech: accessori eleganti e al contempo funzionali. La piattaforma è progettata per offrire agli utenti un'esperienza di navigazione intuitiva e personalizzata, facilitando la scoperta e l'acquisto di prodotti che uniscono stile e tecnologia.
Entità del sistema e le loro relazioni
1.Utenti
Gli utenti sono la componente umana che interagisce con la nostra applicazione. Essi possono:
•	Aggiungere al Carrello: Gli utenti possono esplorare il catalogo e aggiungere prodotti ed articoli a un carrello temporaneo.
•	Visualizzare il Carrello: Gli utenti possono visualizzare il carrello temporaneo in cui vengono inseriti i prodotti, permettendo loro di controllare e verificare la selezione.
•	Modificare il Carrello: Gli utenti hanno la possibilità di modificare il carrello, aggiungendo o rimuovendo prodotti in base alle loro preferenze.
•	Confermare l’Ordine del Carrello: Una volta soddisfatti delle loro scelte, gli utenti possono confermare l’ordine contenente i prodotti temporanei nel carrello, inviando l’ordine al negozio per la preparazione e spedizione.
•	Registrarsi: Gli utenti hanno la possibilità di registrarsi nella piattaforma attraverso nome utente e password, permettendo una personalizzazione dell’esperienza e una gestione più agevole degli ordini.
•	Accedere: Gli utenti, se precedentemente registrati, possono accedere utilizzando le loro credenziali, facilitando l'accesso a funzioni personalizzate e allo storico degli ordini.
2.Amministratore(admin)
L'amministratore è un utente con privilegi speciali che può:
•	Eseguire Azioni di Utente Comune: L'amministratore può eseguire tutte le azioni che un utente comune può compiere, come la gestione del carrello e la conferma degli ordini, permettendo un controllo diretto sulle operazioni standard dell'applicazione.
•	Gestire il Catalogo Prodotti: L'amministratore può aggiungere, modificare o rimuovere prodotti dal catalogo, assicurando che l'offerta sia sempre aggiornata e pertinente.
•	Gestire Ordini: L'amministratore può monitorare, aggiornare lo stato e risolvere eventuali problematiche relative agli ordini, garantendo un processo di acquisto e consegna senza intoppi.
3.Negozio
Il cuore del programma è rappresentato dal negozio, l’entità che gestisce i prodotti e gli ordini, mettendoli in relazione con gli utenti. Il negozio può:
•	Ricevere Ordini: Ricevere ordini dai carrelli pieni dei prodotti selezionati dagli utenti. Ogni ordine contiene dettagli specifici sui prodotti e le quantità richieste.
•	Gestire Ordini: Gestire gli ordini confermati dagli utenti, pronti per essere preparati e spediti. Questo include la verifica della disponibilità dei prodotti, l'elaborazione e l'organizzazione per la spedizione.
•	Confermare e Spedire Ordini: Notificare agli utenti quando i loro ordini risultano pronti e confermare la spedizione. Questo può includere l'invio di notifiche tramite e-mail o altri canali di comunicazione, fornendo informazioni sul tracciamento della spedizione.

Tecnologie Utilizzate
L'applicazione è stata progettata utilizzando una combinazione di tecnologie moderne per garantire efficienza, scalabilità e un'esperienza utente di alta qualità.
Database
Per la gestione dei dati, è stato scelto MySQL, un database relazionale noto per la sua affidabilità, performance elevate e capacità di gestire grandi volumi di dati. MySQL offre un'ampia gamma di funzionalità avanzate, tra cui supporto per transazioni, robusta integrità dei dati e ottime capacità di query, rendendolo ideale per le necessità del nostro sistema.
Backend
Il linguaggio principale utilizzato per lo sviluppo del backend è C#, sfruttando il framework .NET. C# è scelto per la sua robustezza, facilità di manutenzione e vasta libreria di strumenti e funzionalità che accelerano lo sviluppo e migliorano la sicurezza e la performance dell'applicazione. Grazie al framework .NET, l'applicazione beneficia di una piattaforma consolidata e di una comunità di supporto attiva.
Frontend
JavaScript è impiegato per migliorare l'interattività e la dinamicità delle pagine web, garantendo un'esperienza utente fluida e reattiva. Anche se utilizzato in minima parte, JavaScript permette di creare interfacce utente più coinvolgenti e di migliorare l'usabilità complessiva dell'applicazione.
Design responsivo
Per la gestione della parte mobile dell'applicazione, viene utilizzato Bootstrap. Questo framework CSS facilita lo sviluppo di interfacce utente flessibili e moderne, assicurando che l'applicazione sia completamente responsiva e ottimizzata per una vasta gamma di dispositivi, inclusi smartphone e tablet. Bootstrap offre una serie di componenti predefiniti e uno stile coerente, accelerando il processo di sviluppo e mantenendo un design professionale.
Struttura dell’applicazione
L'architettura dell'applicazione segue il pattern MVC (Model-View-Controller). Questo paradigma di progettazione separa la logica dell'applicazione (Model), la presentazione dell'interfaccia utente (View) e il controllo del flusso delle applicazioni (Controller). L'approccio MVC facilita una gestione del codice più chiara e manutenibile, migliorando la modularità e consentendo una facile estensione e scalabilità futura dell'applicazione.

Come funziona concretamente l’app?
L'applicazione offre una serie di funzionalità pratiche, ideate per gestire un negozio online di articoli tecnologici innovativi. Gli utenti possono registrarsi e accedere con le loro credenziali per iniziare a utilizzare tutte le funzionalità offerte. Una volta effettuato il login, gli utenti verranno reindirizzati alla home page dell'app, da cui potranno esplorare e interagire con il catalogo del negozio.

Gli utenti hanno la possibilità di visualizzare i prodotti disponibili nel negozio, leggendo dettagliate descrizioni, caratteristiche tecniche, e specifiche di ciascun articolo.
 La navigazione intuitiva consente inoltre una facile ricerca e filtraggio dei prodotti in base a diverse categorie e parametri.

Una delle funzionalità chiave dell'app è il carrello degli acquisti, dove gli utenti possono aggiungere i prodotti di loro interesse. Questo carrello è dinamico, permettendo di aggiungere o rimuovere articoli in qualsiasi momento prima di finalizzare l'acquisto. Gli utenti possono anche salvare i loro carrelli per una futura consultazione, rendendo il processo di acquisto flessibile e personalizzabile.

Quando l'utente decide di confermare l'ordine, può farlo tramite un pulsante dedicato. Dopo la conferma, l'ordine viene inviato al negozio, che si occuperà di elaborarlo e inviare una notifica di ricezione all'utente. Questa notifica confermerà che l'ordine è stato preso in carico e fornirà informazioni sui tempi di spedizione e consegna.

L'applicazione include anche un sistema di gestione degli ordini, dove gli utenti possono monitorare lo stato dei loro acquisti, visualizzare lo storico degli ordini e ricevere aggiornamenti in tempo reale.
Conclusione
In conclusione, l'applicazione sviluppata rappresenta un efficace strumento di vendita online per prodotti tecnologici, inclusi articoli innovativi e di nicchia come anelli e braccialetti smart. Questa piattaforma offre una solida base per il commercio elettronico, aprendo nuove opportunità sia per i venditori che per i clienti alla ricerca di prodotti tecnologici avanzati.
Le potenzialità di crescita dell'applicazione sono significative. In futuro, sarà possibile integrare ulteriori funzionalità per migliorare l'esperienza utente e la gestione delle vendite. Tra le implementazioni future più promettenti, possiamo includere:
-  Supporto clienti live: Un sistema di assistenza in tempo reale tramite chat o videochiamate, che consentirà agli utenti di ricevere immediatamente risposte alle loro domande e risolvere rapidamente eventuali problemi.
-  Personalizzazione dell'esperienza utente: Funzionalità avanzate che permetteranno di offrire suggerimenti personalizzati basati sulle preferenze e sulla cronologia degli acquisti degli utenti, migliorando così la soddisfazione e la fidelizzazione del cliente.
-  Recensioni e feedback: Un sistema di recensioni più interattivo, che permetta agli utenti di lasciare feedback dettagliati e valutazioni sui prodotti acquistati, contribuendo a creare una comunità attiva e informata.
-  Integrazione con social media: Funzionalità che permettano agli utenti di condividere i loro acquisti e le loro esperienze sui principali social network, aumentando la visibilità del negozio e attraendo nuovi clienti.
-  Programmi di fedeltà e promozioni: Implementazione di programmi di fedeltà che offrano sconti, punti e promozioni esclusive agli utenti più assidui, incentivando così gli acquisti ripetuti.
L'integrazione di queste funzionalità non solo migliorerà l'esperienza complessiva degli utenti, ma aumenterà anche la competitività del negozio online nel mercato tecnologico. Con una continua attenzione all'innovazione e alla qualità del servizio, l'applicazione ha il potenziale per diventare un punto di riferimento nel settore delle vendite online di articoli tecnologici di tendenza.



