Feature: Tickets Behavior
	In order to verify the behavior of `Tickets Controller`
	As a `Consumer`
	I want to verify if it behaves as per the expectations

Scenario: Verify Ticket Creation
	Given A Conversation
	When I create a new Ticket
	Then A Ticket should have been created
	And Its status should be Unassigned
	And It should have 1 conversation
	And It should have 0 notes

Scenario: Read Ticket by Id
	Given A ticket id
	When Reading ticket by a valid id
	Then It should return a ticket

Scenario: Add Conversation
	Given A ticket id
	And A Conversation
	When A conversation is added
	Then Conversation is available in the ticket

Scenario: Add Note
	Given A ticket id
	And A Note
	When A note is added
	Then Note is available in the ticket

Scenario Outline: Update Ticket Status
	Given A ticket id
	When Ticket status is set to <status>
	Then Ticket status should be <status>
	Examples: 
	| status |
	| 0      |
	| 1      |
	| 2      |
	| 3      |
	| 4      |