/*
 * Copyright (C) 2018 Marin Peko
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

#include <Arduino.h>
#include <SoftwareSerial.h>
#include <StateMachine.h>
#include <MessageParser.h>

#define DATA_RATE       9600
#define BLE_MODULE_RX   2
#define BLE_MODULE_TX   3

SoftwareSerial bleModule(BLE_MODULE_RX, BLE_MODULE_TX);
SecureBLE::StateMachine *stateMachine;
SecureBLE::MessageParser parser;

void setup()
{
    /**
     * begin serial port communication
     * and set the data rate
     */
    Serial.begin(DATA_RATE);

    /**
     * begin BLE serial port communication
     * and set the data rate
     */
    bleModule.begin(DATA_RATE);

    stateMachine = new SecureBLE::StateMachine(bleModule);

    while (!Serial);
    Serial.println("Arduino Uno Board Started");
}

void loop()
{
    /**
     * read data from the BLE module
     * and pass it to the parser
     */
    while (bleModule.available())
    {
        char character = bleModule.read();
        parser.buildMessage(character);

        if (parser.isMessageEnded())
        {
            parser.run();
            stateMachine->onReceive(parser.getMessageType(),
                                    parser.getMessageContent());
            parser.reset();
        }
    }
}
