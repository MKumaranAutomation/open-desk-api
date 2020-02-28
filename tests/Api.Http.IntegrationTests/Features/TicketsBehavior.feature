Feature: Tickets Behavior
	In order to verify the behavior of `Tickets Controller`
	As a `Consumer`
	I want to verify if it behaves as per the expectations

Scenario: Able to create new ticket
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	Then The ticket should have 1 conversation and 0 notes and status should be Unassigned