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

#ifndef StateMachine_H
#define StateMachine_H

#include <SoftwareSerial.h>
#include <ECDHKeyExchange.h>
#include <STS.h>

namespace SecureBLE
{

enum State
{
    STATE_UNKNOWN = 0,
    STATE_START,
    STATE_KEYS_GENERATION,
    STATE_SHARED_SECRET_GENERATION,
    STATE_SIGNATURE_VERIFICATION,
    STATE_ENCRYPTED_CONNECTION
};

enum Event
{
    EVENT_UNKNOWN = 0,
    EVENT_CONNECT_REQ,
    EVENT_PU_KEY_RECEIVED,
    EVENT_SHARED_SECRET_FAILURE,
    EVENT_ENCRYPTED_SIGNATURE_RECEIVED,
    EVENT_SIGNATURE_VERIFIED,
    EVENT_SIGNATURE_NOT_VERIFIED,
    EVENT_DATA,
    EVENT_RESET
};

class StateMachine
{
public:
    StateMachine(SoftwareSerial const& bleModule);
    ~StateMachine() noexcept;

    void onReceive(char const *messageType, char const *messageContent);

private:
    void switchState(State newState);
    void onEntry();

    Event messageTypeToEvent(char const *messageType);

    /* message types */
    static char const CONNECT_MESSAGE_TYPE[];
    static char const PU_MESSAGE_TYPE[];
    static char const FAILURE_MESSAGE_TYPE[];
    static char const SIG_MESSAGE_TYPE[];
    static char const SIGVER_MESSAGE_TYPE[];
    static char const SIGNVER_MESSAGE_TYPE[];
    static char const DATA_MESSAGE_TYPE[];
    static char const RESET_MESSAGE_TYPE[];

    State m_currentState;
    SoftwareSerial m_bleModule;
    ECDHKeyExchange m_ECDHKeyExchange;
    STS m_sts;
};

} /* SecureBLE */

#endif /* StateMachine_H */
