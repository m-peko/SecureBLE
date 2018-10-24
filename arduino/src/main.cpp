#include <Arduino.h>
#include <SoftwareSerial.h>

#define LED_BUILTIN 13

SoftwareSerial ble(2, 3); /* RX, TX */

void setup() {
    /* initialize LED digital pin as output */
    pinMode(LED_BUILTIN, OUTPUT);

    /* open serial port */
    Serial.begin(9600);

    /* begin bluetooth serial port communication */
    ble.begin(9600);
}

void loop() {
    /* turn the LED on (HIGH is the voltage level) */
    digitalWrite(LED_BUILTIN, HIGH);

    /* wait for 1s */
    delay(1000);

    /* turn the LED off (LOW is the voltage level) */
    digitalWrite(LED_BUILTIN, LOW);

    /* wait for 1s */
    delay(1000);

    Serial.println("Sending Bluetooth Message...");
    ble.write("Testing...");
    delay(500);
}