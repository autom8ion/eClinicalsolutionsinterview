
Feature: Careers application flow
  As a candidate
  I want to search for automation roles
  So that I can validate the application form shows required errors when blank

  @smoke @application
  Scenario: Search 'automation' and validate blank application errors
    Given I am on the home page
    When I search careers for automation
    And I open the job result by title Test Automation Architect
    And I start the application process with blank details
    Then I should see required field errors for a blank application
    
 @parallel
  Scenario: Search 'automation' on careers (<browser>)
    Given I am on the home page
    When I search careers for automation
    And I open the job result by title Test Automation Architect
    And I start the application process with blank details
    Then I should see required field errors for a blank application

    Examples:
      | browser |
      | chrome  |
      | edge    |