#include <Arduino.h>

#define LED_BUILTIN 13

void setup() {
    /* initialize LED digital pin as output */
    pinMode(LED_BUILTIN, OUTPUT);
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
}