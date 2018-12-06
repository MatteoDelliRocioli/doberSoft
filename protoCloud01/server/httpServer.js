var restify = require('restify');
var influx = require ('../connector.js');
//var connector = require('./db/connector');
//TODO scompattare i dati e scriverli su un db, usare un oggetto per chiamare la funzione di scrittura, instanziare quindi ua classe per l'oggetto che scrive sul db
var server = restify.createServer();
server.use(restify.plugins.bodyParser());
// ciao da andrea
/*const Influx = require('influx');
const influx = new Influx.InfluxDB({
    host: 'localhost',
    database: 'db_icar',
    schema: [ //schema di dati da mettere dentro influx
        {
            measurement: 'temperature',
            fields: {
                name: Influx.FieldType.STRING,
                value: Influx.FieldType.INTEGER
            },
            tags: [
                'tagTest'
            ]
        },
        {
            measurement: 'CarId',
            fields: {
                single_detection: Influx.FieldType.STRING,
                value1: Influx.FieldType.INTEGER
            },
            tags: [
                'SensorId',
                ''
            ]
        }
        /*{
            measurement: 'CarId',
            fields: {
                brand: Influx.FieldType.STRING,
                model: Influx.FieldType.STRING,
                plate: Influx.FieldType.STRING
            },
            tags: [
                'SensorId'
            ]
        }*/

/*
    ]
});*/

server.get('/cars', function(req, res, next) {
    res.send('List of cars: [TODO]');
    return next();
});

//server.get('/cars/:CarId/data', function(req, res, next) {
server.get('/cars/:CarId', function(req, res, next) {
    res.send('Current values for car ' + req.params['CarId'] + ': [TODO]');
    //TODO returns the car details
    return next();
});

server.get('/cars/:CarId/:SensorId', function (req, res, next) {
    res.send('Current values for car[' + req.params['CarId'] + ']\sensor[' + req.params['SensorId'] + ']: [TODO]');
    //TODO returns sensor's data
    return next();
});

//server.post('/cars/:plate', function(req, res, next) {
//    res.send('Data received from plate [TODO]');

//    // uncomment to see posted data
//    //console.log(req.body);

//    return next();
//});


server.post('/cars/:CarId/:SensorId', function (req, res, next) {

    // uncomment to see posted data
    //console.log('CarId', CarId,'SensorId',SensorId,req.body);
    console.log(req.body);
    console.log('CarId: ', req.params.CarId, ', SensorId: ', req.params.SensorId, ': TODO'); //post paradigm
    //console.log(req.body.name);

    res.send(201);

    influx.writePoints([
        {
            measurement: 'temperature',
            tags: { tagTest: req.body.tagTest },
            fields: { name: req.body.name, value: req.body.value }
        },
        {
            measurement: 'CarId',
            tags: { SensorId: req.body.SensorId }, //modo in cui devono essere mandati i dati
            fields: { single_detection: req.body.single_detection, value1: req.body.value }
        }
    ]);
    //res.statusCode(201);
    //res.send('Data received from plate [TODO]');

    return next();
});

/**
 * */
server.post('/cars/:CarId/sensors/:sensorType/:SensorId', function (req, res, next) {

    // uncomment to see posted data
    //console.log('CarId', CarId,'SensorId',SensorId,req.body);
    console.log(req.body);
    console.log('CarId: ', req.params.CarId, ', sensorId: ', req.params.SensorId, ': TODO'); //post paradigm
    //console.log(req.body.name);

    res.send(201);

    /*influx.writePoints([
        {
            measurement: 'temperature',
            tags: { tagTest: req.body.tagTest },
            fields: { name: req.body.name, value: req.body.value, time: req.body.timestamp }
        },
        {
            measurement: 'CarId',
            tags: { SensorId: req.body.SensorId }, //modo in cui devono essere mandati i dati
            fields: { single_detection: req.body.single_detection, value1: req.body.value }
        }
    ]);*/
    //res.statusCode(201);
    //res.send('Data received from plate [TODO]');

    return next();
});

server.listen(8080, function() {
    console.log('%s listening at %s', server.name, server.url);
});


