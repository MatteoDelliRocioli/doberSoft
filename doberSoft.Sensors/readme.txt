Sensore generico, dotato di interfaccia ISensor
in fase di configurazione riceve:
	- insieme di IInput da cui preleverà i dati
	- regole di scansione > imposta tmr***_trig()
	- scaling function

espone
	.On()				avvia il sensore
	.Off()				arresta il sensore
	.GetValue()			restistuisce il valore scalato
produce
	->Changed()	evento rilasciato quando le regole applicate al valore lo impongono

privato
	.tmrScan			timer settato in base alle regole
						
	metodi in cui ricevere tm***_trig() per scatenare Change 
	tmrPoll_trig()		in base al polling rate
	tmrPush_trig()		in base alle regole
	{
		evthandler.change()
	}

	GetValue()
	{
		return scaleFnc(inputs)
	}
	

	aritmetica con i generics:
	https://www.codeproject.com/Articles/992340/%2FArticles%2F992340%2FGeneric-Math-in-Csharp-Using-Runtime-Compilation