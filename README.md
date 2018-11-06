# <p align="center">ECDHoBLE</p>
<p align="center">Elliptic-curve Diffie-Hellman key exchange between Arduino microcontroller and Android application over BLE.</p>

### Key exchange protocol

<p align="center">
  <img alt="Key exchange protocol" src="https://github.com/m-peko/ECDHoBLE/blob/master/docs/resources/KeyExchangeProtocol.jpg" height="300px"/>
</p>

- Arduino Uno board as a user A selects its private key **PRarduino**, an integer less than the order *n*, and generates public key **PUarduino** which belongs to a point on elliptic curve
- Android app as a user B selects its private key **PRandroid**, an integer less than the order *n*, and generates public key **PUandroid** which belongs to a point on elliptic curve
- after exchanging each other's public key, both, Arduino Uno board and Android app, generate shared secret **K** which is then used for secure communication

Refer to this [article](http://www.ieeesmc.org/newsletters/back/2010_12/main_article3.html) about security mechanism for clustered wireless sensor networks based on elliptic curve cryptography.

### Bluetooth Low Energy (BLE)

BLE or Bluetooth Low Energy is a wireless personal area network technology.

<p align="center">
  <img alt="BLE Stack" src="https://github.com/m-peko/ECDHoBLE/blob/master/docs/resources/BleStack.jpg" height="300px"/>
</p>

**GAP (Generic Access Profile)** is the layer of the BLE stack which determines the network topology of the BLE system. Based on this layer, there are two roles in the BLE communication: **BLE GAP Central** and **BLE GAP Peripheral**.

The GAP Central is typically the device which initiates the connection with the GAP Peripheral. Once the two devices are connected, they will perform a “pairing” process where they will exchange the information necessary to establish an encrypted connection.

**GATT (Generic Attribute Profile)** is the layer of the BLE stack used by the application for data communication between two connected devices. Data is passed and stored in the form of characteristics which are stored in memory of BLE device. When two devices are connected, they obtain one of two roles: **GATT Server** and **GATT Client**.

The GATT Server is the device containing the characteristic database that is being read or written by a GATT Client. Consider characteristics as groups of information called attributes.

A typical characteristic is composed of the following attributes:

- Characteristic Value
- Characteristic Declaration
- Client Characteristic Configuration
- Characteristic User Description

Refer to this [article](http://dev.ti.com/tirex/content/simplelink_cc2640r2_sdk_1_40_00_45/docs/blestack/ble_user_guide/html/ble-stack-3.x/gatt.html) about GATT layer of the BLE protocol stack.

#### Specs

- \> 100m range
- 1 Mbit/s over the air data rate
- not specified number of active slaves
- 6ms latency

#### Security 

***Pairing*** is the process by which two BLE devices exchange device information so that secure link can be established. The process varies somewhat between the BLE 4.2 devices and the older 4.1 and 4.0 devices.

In BLE 4.2, devices use ECDH key exchange in their pairing process. The only problem with ECDH key exchange is that *it does not give the user a way to verify the authenticity of the connection*. Therefore, it is still vulnerable to MITM attacks.

#### Security issues

- passive eavesdropping
- MITM attacks
- identity tracking

Refer to this [article](https://www.digikey.com/eewiki/display/Wireless/A+Basic+Introduction+to+BLE+Security) about BLE security. 

#### Comparisons between BLE, WiFi and LoRa

- [BLE vs WiFi](https://hackernoon.com/ble-vs-wi-fi-a-comparison-of-wireless-technology-for-iot-product-development-1c7be179f379)
- [BLE vs LoRa](https://www.mwrf.com/systems/lorable-puts-iot-everywhere-map)
- [WiFi vs LoRa](https://medium.com/bytes-io/lora-vs-wifi-3-questions-d9c93137fca)

### State Machine

State Machines in general represent a set of complex rules and conditions. For the purpose of this project, State Machine is created in order to simplify execution of Elliptic-curve Diffie-Hellman key exchange protocol and also to simulate its states and transitions. The same State Machine is implemented on both, Arduino Uno board and Android device.

<p align="center">
  <img alt="State Machine" src="https://github.com/m-peko/ECDHoBLE/blob/master/docs/resources/StateMachine.jpg" height="300px"/>
</p>

### Messages

All messages exchanged during both, key exchange protocol and encrypted communication, have the following template: `$<MESSAGE_TYPE>=<MESSAGE_CONTENT>;`. `MESSAGE_CONTENT` part of the message is optional.

Supported message types:
- **CONNECT** - initiates ECDH key exchange protocol
- **PU** - carries ECDH public key from one entity to another
- **SUCCESS** - indication for successful generation of a shared secret
- **FAILURE** - indication for unsuccessful generation of a shared secret
- **DATA** - carries data from one entity to another
- **RESET** - resets ECDH key exchange protocol

### Arduino

For the purpose of this project, Arduino Uno is used along with Bluetooth module HM-10.

Wiring the HM-10 to the Arduino Uno board:

| Arduino Uno | HM-10 |
|:-----------:|:-----:|
|      D2     |   TX  |
|      D3     |   RX  |
|     GND     |  GND  |
|    3.3V     |  VCC  |

#### Arduino crypto libraries

- [Arduino Cryptography Library](https://rweather.github.io/arduinolibs/index.html)

### Android

For the purpose of this project, Android application is built by using C# and Xamarin.

#### Android crypto libraries

 - [Example](https://github.com/joelwass/Android-BLE-Connect-Example)
