Feature: Widgets

As a User
I want to interact with various widgets of the application such as Auto Complete, Progress Bar
So that I can ensure that interactions are captured and processed correctly.

Background: 
	Given I'm on the Home page of Demoqa 
	And I navigate to the "Widgets" section

@AutoComplete
Scenario: Verify correct behaviour of Auto complete function
	When I navigate to the "Auto Complete" subsection
	And I enter "g" in the Type multiple color names field
	Then the auto complete should suggest 3 variants
	And all displayed suggestions should contain the letter "g"

@AutoComplete 
Scenario: Verify addition and deletion of entered data
	When I navigate to the "Auto Complete" subsection
	And I enter the following colors and accept suggestions in the Type multiple color names field:
		| Value  |
		| Red    |
		| Yellow |
		| Green  |
		| Blue   |
		| Purple |
	And I delete the following colors
		| Value  |
		| Yellow |
		| Purple |
	Then the Type multiple color names field should display the following colors
		| Value |
		| Red   |
		| Green |
		| Blue  |

@ProgressBar
Scenario: Verify progress bar behaviour
	When I navigate to the "Progress Bar" subsection
	And I press "Start" button and wait for the progress bar to reach "100%"
	Then the button should change to "Reset"
	
	When I press "Reset" button
	Then the button should change to "Start"
	And the progress bar should show "0%" completion