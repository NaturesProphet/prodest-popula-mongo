#!/usr/bin/env node
const rabbitHost = process.env.RABBIT_HOST || 'localhost';
const rabbitTopic = process.env.RABBIT_TOPIC || 'CETURB';
const chave = process.env.CHAVE_DE_ROTEAMENTO || '#';



var amqp = require( 'amqplib/callback_api' );

amqp.connect( `amqp://${rabbitHost}`, function ( err, conn ) {




    conn.createChannel( function ( err, ch ) {
        var topico = rabbitTopic;
        var key = chave;
        ch.assertExchange( topico, 'topic', { durable: true } );
        console.log( "enviando..." );
        for ( var i = 0; i < 500000; i++ ) {
            var msg = `{ indice: ${i}}`
            ch.publish( topico, key, new Buffer( msg ), { persistent: true } );
            //console.log( `[ RABBIT ]   Mensagem "${msg}" enviada ao topico "${topico}" em "${rabbitHost}" com a chave "${key}".\n` )
        }
        console.log( "tudo enviado" );
    } );
    setTimeout( function () { conn.close(); process.exit( 0 ) }, 500 );
} );













// codigo do exemplo do tutorial work-queues
// var amqp = require( 'amqplib/callback_api' );

// amqp.connect( 'amqp://localhost', function ( err, conn ) {
//     conn.createChannel( function ( err, ch ) {
//         var q = 'task_queue';
//         var msg = process.argv.slice( 2 ).join( ' ' ) || "Hello World!";

//         ch.assertQueue( q, { durable: true } );
//         ch.sendToQueue( q, new Buffer( msg ), { persistent: true } );
//         console.log( " [x] Sent '%s'", msg );
//     } );
//     setTimeout( function () { conn.close(); process.exit( 0 ) }, 500 );
// } );
