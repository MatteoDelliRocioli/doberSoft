in fase di configurazione riceve
- input layer
- regole di delivery
- output layer

qui disegnamo la topologia del sistema
- istanziando i sensori e gli iniettiamo gli inputs
- quando riceviamo le notifiche di cambiamento dai sensori
	costruiamo i messaggi in base alle regole di delivery (con code ecc.)
	invimo i messaggi al layer di uscita
-------------
sensor_change()
{
	msg = build(sensor.getValue())
	outputLayer.sendMessage(msg)
}