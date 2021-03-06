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

#include <StateMachine.h>

namespace SecureBLE
{

char const StateMachine::CONNECT_MESSAGE_TYPE[] = "CONNECT";
char const StateMachine::PU_MESSAGE_TYPE[]      = "PU";
char const StateMachine::FAILURE_MESSAGE_TYPE[] = "FAILURE";
char const StateMachine::SIG_MESSAGE_TYPE[]     = "SIG";
char const StateMachine::SIGVER_MESSAGE_TYPE[]  = "SIGVER";
char const StateMachine::SIGNVER_MESSAGE_TYPE[] = "SIGNVER";
char const StateMachine::DATA_MESSAGE_TYPE[]    = "DATA";
char const StateMachine::RESET_MESSAGE_TYPE[]   = "RESET";

StateMachine::StateMachine(SoftwareSerial const& bleModule)
    : m_currentState(State::STATE_START),
      m_bleModule(bleModule)
{}

StateMachine::~StateMachine()
{}

void
StateMachine::onReceive(char const *messageType, char const *messageContent)
{
    Event event = messageTypeToEvent(messageType);

    switch (event)
    {
    case Event::EVENT_CONNECT_REQ:
        if (State::STATE_START == m_currentState)
        {
            switchState(State::STATE_KEYS_GENERATION);
        }
        break;
    case Event::EVENT_PU_KEY_RECEIVED:
        if (State::STATE_KEYS_GENERATION == m_currentState)
        {
            m_ECDHKeyExchange.setForeignPublicKey(messageContent);
            switchState(State::STATE_SHARED_SECRET_GENERATION);
        }
        break;
    case Event::EVENT_SHARED_SECRET_FAILURE:
        if (State::STATE_SHARED_SECRET_GENERATION == m_currentState)
        {
            switchState(State::STATE_START);
        }
        break;
    case Event::EVENT_ENCRYPTED_SIGNATURE_RECEIVED:
        if (State::STATE_SHARED_SECRET_GENERATION == m_currentState)
        {
            m_sts.setForeignSignature(messageContent);
            switchState(State::STATE_SIGNATURE_VERIFICATION);
        }
        break;
    case Event::EVENT_SIGNATURE_VERIFIED:
        if (State::STATE_SIGNATURE_VERIFICATION == m_currentState)
        {
            switchState(State::STATE_ENCRYPTED_CONNECTION);
        }
        break;
    case Event::EVENT_SIGNATURE_NOT_VERIFIED:
        if (State::STATE_SIGNATURE_VERIFICATION == m_currentState)
        {
            switchState(State::STATE_START);
        }
        break;
    case Event::EVENT_DATA:
        // TODO(m-peko): Set data and decrypt it with ECDH shared secret
        switchState(State::STATE_ENCRYPTED_CONNECTION);
        break;
    case Event::EVENT_RESET:
        switchState(State::STATE_START);
        break;
    default:
        /* unknown event */
        break;
    }
}

void
StateMachine::switchState(State newState)
{
    m_currentState = newState;
    onEntry();
}

void
StateMachine::onEntry()
{
    switch (m_currentState)
    {
    case State::STATE_START:
        m_ECDHKeyExchange.clearKeys();
        break;
    case State::STATE_KEYS_GENERATION:
        m_ECDHKeyExchange.generateKeys();

        /* publish ECDH public key  */
        m_bleModule.println("$PU=");
        m_bleModule.println(m_ECDHKeyExchange.getPublicKeyStr());
        m_bleModule.println(";");

        break;
    case State::STATE_SHARED_SECRET_GENERATION:
        if (m_ECDHKeyExchange.generateSharedSecret())
        {
            // TODO(m-peko): Create signature and send it
        }
        else
        {
            m_bleModule.println("$FAILURE;");
        }
        break;
    case State::STATE_SIGNATURE_VERIFICATION:
        if (m_sts.verifyForeignSignature())
        {
            m_bleModule.println("$SIGVER;");
        }
        else
        {
            m_bleModule.println("$SIGNVER;");
        }
        break;
    case State::STATE_ENCRYPTED_CONNECTION:
        /* data received */
        // TODO(m-peko): Decrypt received data with ECDH shared secret
        break;
    case State::STATE_UNKNOWN:
    default:
        /* unknown state */
        break;
    }
}

Event
StateMachine::messageTypeToEvent(char const *messageType)
{
    if (!strcmp(messageType, CONNECT_MESSAGE_TYPE))
    {
        return Event::EVENT_CONNECT_REQ;
    }
    else if (!strcmp(messageType, PU_MESSAGE_TYPE))
    {
        return Event::EVENT_PU_KEY_RECEIVED;
    }
    else if (!strcmp(messageType, FAILURE_MESSAGE_TYPE))
    {
        return Event::EVENT_SHARED_SECRET_FAILURE;
    }
    else if (!strcmp(messageType, SIG_MESSAGE_TYPE))
    {
        return Event::EVENT_ENCRYPTED_SIGNATURE_RECEIVED;
    }
    else if (!strcmp(messageType, SIGVER_MESSAGE_TYPE))
    {
        return Event::EVENT_SIGNATURE_VERIFIED;
    }
    else if (!strcmp(messageType, SIGNVER_MESSAGE_TYPE))
    {
        return Event::EVENT_SIGNATURE_NOT_VERIFIED;
    }
    else if (!strcmp(messageType, DATA_MESSAGE_TYPE))
    {
        return Event::EVENT_DATA;
    }
    else if (!strcmp(messageType, RESET_MESSAGE_TYPE))
    {
        return Event::EVENT_RESET;
    }
    else
    {
        return Event::EVENT_UNKNOWN;
    }
}

} /* SecureBLE */
