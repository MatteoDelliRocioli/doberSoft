Sensore generico, dotato di interfaccia ISensor
in fase di configurazione riceve:
	- insieme di IInput da cui preleverà i dati
	- regole di scansione > imposta tmr***_trig()
	- scaling function

espone
	.On()				avvia il sensore
	.Off()				arresta il sensore
	.GetValue()			retistuisce il valore scalato
produce
	->Changed()	evento rilasciato quando le regole applicate al valore lo impongono

privato
	.tmrScan			timer settato in base alle regole
						
	metodi in cui ricevere tm***_trig() per scatenare Change 
	tmrPoll_trig()		in base al polling rate
	tmrPush_trig()		in base alle regole
	{
		evthandler.chang()
	}

	GetValue()
	{
		return scaleFnc(inputs)
	}
	
