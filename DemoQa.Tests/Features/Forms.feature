Feature: Forms

As a User
I want to fill various forms of the application such as Practice Form
So that I can ensure that submissions are captured and processed correctly.

Background: 
	Given I'm on the Home page of Demoqa 
	And I navigate to the "Forms" section

@PracticeForm
Scenario: Verify form submit and return
	When I navigate to the "Practice Form" subsection
	And I fill the form with the following data:
	| Field         | Value          |
	| First Name    | Teresa         |
	| Last Name     | Tester         |
	| Email         | terry@test.org |
	| Gender        | Female         |
	| Mobile        |     0123456789 |
	| Date of Birth | 01 Jan 2000    |
	| Subjects      | Physics, Maths |
	| Hobbies       | Reading, Music |
	| State         | Uttar Pradesh  |
	| City          | Merrut         | 
	And I submit the form
	Then the output area should display the submitted information
