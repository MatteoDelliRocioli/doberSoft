/*implementare connessione al db, guarda sul quaderno*/
//export dell funzione


const Influx = require('influx');

//dichiarare metodo che contiene schema


const influx = new Influx.InfluxDB({
    host: 'localhost',
    database: 'db_icar',
    schema: [ //schema di dati da mettere dentro influx
        {
            measurement: 'temperature',
            fields: {
                type: Influx.FieldType.STRING,
                id: Influx.FieldType.STRING,
                value: Influx.FieldType.INTEGER
            },
            tags: [
                'tagTest'
            ]
        },
        {
            measurement: 'IdCar',
            fields: {
                type: Influx.FieldType.STRING,
                single_detection: Influx.FieldType.STRING,
                value1: Influx.FieldType.INTEGER
            },
            tags: [
                'idSensor'
            ]
        }
        /*{
            measurement: 'idCar',
            fields: {
                brand: Influx.FieldType.STRING,
                model: Influx.FieldType.STRING,
                plate: Influx.FieldType.STRING
            },
            tags: [
                'IdSensor'
            ]
        }*/


    ]
});

module.exports = influx;