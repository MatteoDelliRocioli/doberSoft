//import * as packetsConfig from "../packetsConfig.json";

var mqtt = require('mqtt');
var client  = mqtt.connect('mqtt://test.mosquitto.org');
var packet = require("../packetsConfig");


client.on('connect', function () {
    client.subscribe('cars/aa123bb/sensors/temperature', function (err) {
        if (!err) {
            console.log("subscribed");
            //client.publish('', 'Hello mqtt')
        }
    })
});

client.on('connect', function () {
    client.publish('cars/aa123bb/sensors/temperature', JSON.stringify(packet), function (err){
        if (!err) {
            console.log("published");
            //client.publish('', 'Hello mqtt')
        }
    })
});

client.on('message', function (topic, message) {
    // message is Buffer
    //console.log(message.toString())
    console.log(message)
    client.end()
});
