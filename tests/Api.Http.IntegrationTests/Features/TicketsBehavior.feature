Feature: Tickets Behavior
	In order to verify the behavior of `Tickets Controller`
	As a `Consumer`
	I want to verify if it behaves as per the expectations

Scenario: Able to create new ticket
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	Then The ticket should have 1 conversation and 0 notes and status should be Unassigned

Scenario: Read ticket by invalid id
	Given A random ticket id
	When Read the ticket
	Then Should return NotFound

Scenario Outline: Update Ticket Status
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	And Update Ticket Status to <status>
	And Read the created ticket
	Then Ticket status should be <status>
	Examples: 
	| status     |
	| Assigned   |
	| UnAssigned |
	| Closed     |
	| Reopened   |
	| Blocked    |

Scenario Outline: Update ticket status with invalid id
	Given A random ticket id
	When Update Ticket Status to <status>
	Then Should return NotFound
	Examples: 
	| status   |
	| Assigned |

Scenario: Add conversation
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	And Add a new conversation
	And Read the created ticket
	Then Ticket should contain added conversation

Scenario Outline: Add conversation with invalid id
	Given A random ticket id
	When Add a new conversation
	Then Should return NotFound

Scenario: Add note
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	And Add a new note
	And Read the created ticket
	Then Ticket should contain added note and its status is not closed

Scenario: Flip note status in a ticket
	Given A Conversation title and content
	When Create a new ticket
	And Read the created ticket
	And Add a new note
	And Set note status to Closed
	And Read the created ticket
	Then Note status is Closed
	