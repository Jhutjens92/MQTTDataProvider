# WEKIT MQTTDataProvider
The WEKIT MQTTDataProvider is an application designed to listen to a specific MQTT Topic, 
filter the data, send the filtered data to predefined/preprogrammed MQTT Topics and send the data to the Learning Hub.

### Prerequisites
- Download the Learning Hub: https://github.com/janschneiderou/LearningHub
- Installed the Mosquitto Broker (local)
- Use an  MQTT Sender for testing 

## Getting Started
The brokeraddress is currently set to "localhost" (127.0.0.1). This means that Mosquitto is required for the program to be able to send and recieve data.
If you want to use any other address use the -ba command (example -ba broker.mqttdashboard.com) as a startup argument.

You can test the MQTTDataProvider without using the Learning Hub by just starting the executable and press "Start Recording"
Currently it does not provide any logging when you run it seperatly. It only shows the received string in the textbox for testing.

If you want to use it with the Learning Hub combined then make you sure have the Learning Hub set up accordingly. 
A complete how to guide can be found here: https://docs.google.com/document/d/1FbTd6wjqa9P_6O51gjZRU2ubiCA94nMZr001NkgBZ5s/edit#

The topics are defined as following:
Receiving topic
Default topic:	 		"wekit/vest"
-----------------------------------------------
Send topics
GSR topic:				"wekit/vest/GSR_Raw"
Pulse topic:			"wekit/vest/Pulse_Raw"
Temperature_internal:	"wekit/vest/Sht0_Temp"	
Temperature_external:	"wekit/vest/Sht1_Temp"
Humidity_internal:		"wekit/vest/Sht0_Hum"
Humidity_external:		"wekit/vest/Sht1_Hum"

## Running the tests
For testing purposes you can use this test string: 
{"client":"WEKIT-VEST-000014A2","time":992953,"imus":[{"ax":-0.04,"ay":1.25,"az":-0.06,"gx":-7.33,"gy":-171.49,"gz":-7.33,"mx":-14.29,"my":5.39,"mz":-3.46,"q0":-0.26,"q1":-0.40,"q2":0.57,"q3":0.67},{"ax":-0.05,"ay":1.23,"az":-0.34,"gx":-7.43,"gy":-171.434,"gz":-7.34,"mx":-14.39,"my":5.39,"mz":-3.36,"q0":-0.36,"q1":-0.30,"q2":0.37,"q3":0.63}],"shts":[{"temp":33,"hum":80},{"temp":21,"hum":55}],"pulse":60,"gsr":1024}

## Authors
* **Jordi Hutjens** -(https://github.com/jhutjens92)