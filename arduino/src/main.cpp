#include <Arduino.h>
#include <SoftwareSerial.h>
#include <Curve25519.h>

#define DATA_RATE     9600
#define LED_BUILTIN   13
#define BLE_MODULE_RX 2
#define BLE_MODULE_TX 3

SoftwareSerial bleModule(BLE_MODULE_RX, BLE_MODULE_TX);

uint8_t publicECDHKey[32];
uint8_t privateECDHKey[32];

void setup()
{
    /* initialize LED digital pin as output */
    pinMode(LED_BUILTIN, OUTPUT);

    /**
     * begin serial port communication and
     * set the data rate
     */
    Serial.begin(DATA_RATE);

    /**
     * begin BLE serial port communication and
     * set the data rate
     */
    bleModule.begin(DATA_RATE);

    while (!Serial);
    Serial.println("Arduino Setup");

    /* generate public and private ECDH key */
    Curve25519::dh1(publicECDHKey, privateECDHKey);
}

void loop()
{
    /* turn the LED on (HIGH is the voltage level) */
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);

    /* turn the LED off (LOW is the voltage level) */
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);

    /* read from the BLE module and write to the Serial */
    if (bleModule.available())
    {
        Serial.write(bleModule.read());
    }

    /* read from the Serial and write to the BLE module */
    if (Serial.available())
    {
        bleModule.write(Serial.read());
    }
}