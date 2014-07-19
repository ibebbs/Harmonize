Feature: Messaging
	In order to allow clients to communicate with me over SignalR
	As Harmonize
	I need messages to be exchanged reliably between client and server

Scenario: Should be able to register an entity
	Given I have started the SignalR endpoint
	And a client has connected
	When the client registers an entity identified as 'TEST'
	Then the entity identified as 'TEST' should have been registered with Harmonize
	
Scenario: Should be able to observe an observable
	Given I have started the SignalR endpoint
	And a client has connected
	And the client has registered an entity identified as 'TEST'
	When the entity identified as 'TEST' observes the observable identified as 'Temperature' on the entity identified as 'OBS'
	Then an observation of the observable identified as 'Temperature' on the entity identified as 'OBS' should have been registered with Harmonize for the entity identified as 'TEST'
	
Scenario: Should be able to receive messages from an observation
	Given I have started the SignalR endpoint
	And a client has connected
	And the client has registered an entity identified as 'TEST'
	And the entity identified as 'TEST' observes the observable identified as 'Temperature' on the entity identified as 'OBS'
	When the following observation is emitted
		| Entity | Observable  | Date                | MeasurementType | Measurement |
		| OBS    | Temperature | 2014-07-19 10:03:00 | Kelvin          | 304.15      |
	Then the following observation should have been received by the client for the identity identified as 'TEST' within '5' seconds
		| Entity | Observable  | Date                | MeasurementType | Measurement |
		| OBS    | Temperature | 2014-07-19 10:03:00 | Kelvin          | 304.15      |
	
Scenario: Should be able to deregister an entity
	Given I have started the SignalR endpoint
	And a client has connected
	And the client has registered an entity identified as 'TEST'
	When the client deregisters the entity identified as 'TEST'
	Then the entity identified as 'TEST' should have been deregistered with Harmonize
