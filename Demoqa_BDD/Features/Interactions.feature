Feature: Interactions

As a User
I want to interact with various elements of the application such as Selectable elements
So that I can ensure that interactions are captured and processed correctly.

Background: 
	Given I'm on the Home page of Demoqa 
	And I navigate to the "Interactions" section

@Selectable
Scenario: Verify selectable grid interactions
	When I navigate to the "Selectable" subsection
	And I navigate to the "Grid" and select squares with the following values:
	| Value |
	| One   |
	| Three |
	| Five  |
	| Seven |
	| Nine  |
	Then the selected items should match the selected values
