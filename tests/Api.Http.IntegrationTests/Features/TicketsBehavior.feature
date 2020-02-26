Feature: Tickets Behavior
	In order to verify the behavior of `Tickets Controller`
	As a `Consumer`
	I want to verify if it behaves as per the expectations

Scenario: Verify `Tickets Count`
	Given Tickets controller
	When I get the number of tickets available
	Then It should return 2147483647
