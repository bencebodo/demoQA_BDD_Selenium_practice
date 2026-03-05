Feature: Elements

As a User
I want to interact with various UI elements of the application such as Text box, Check box, Web tables, Buttons
So that I can ensure that data is captured and processed correctly.

Background: 
	Given I'm on the Home page of Demoqa 
	And I navigate to the "Elements" section

@TextBox
Scenario: Verify text submit and return
	When I navigate to the "Text Box" subsection
	And I fill the form with the following data:
        | Field             | Value               |
        | Full Name         | John Doe            |
        | Email             | john.doe@email.com  |
        | Current Address   | DoeTown, John st 2. |
        | Permanent Address | DoeTown, John st 2. |
	And I submit the form
	Then the output area should display the submitted information

@CheckBoxScenario
Scenario: Verify cascading and Leaf Node selection 
    When I navigate to the "Check Box" subsection 
    And I select the following folders in the tree:
      | Folder Path                            |
      | Home > Desktop                         |
      | Home > Documents > WorkSpace > Angular |
      | Home > Documents > WorkSpace > Veu     |
      | Home > Documents > Office              |
      | Home > Downloads                       |
    Then the output area should display the following selected items:
      | Item       |
      | desktop    |
      | notes      |
      | commands   |
      | angular    |
      | veu        |
      | public     |
      | private    |
      | classified |
      | general    |
      | office     |
      | downloads  |
      | wordFile   |
      | excelFile  |

@WebTables
Scenario: Verify data deletion function
	When I navigate to the "Web Tables" subsection
	And I delete the row where the "First Name" column shows "Alden"
	Then the table should display 2 rows
	And "Compliance" should not be displayed in "Department" column

@Buttons
Scenario Outline: Verify button behaviour
	When I navigate to the "Buttons" subsection
	And I perform the required action to "<ButtonName>" button
	Then the message "<ExpectedMessage>" should be displayed

	Examples: 
		| ButtonName      | ExpectedMessage               |
		| Double Click Me | You have done a double click  |
		| Right Click Me  | You have done a right click   |
		| Click Me        | You have done a dynamic click |
