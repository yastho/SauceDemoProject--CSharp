Feature: Login
  As a SauceDemo user
  I want to log in
  So that I can access the inventory page
 @Positve
  Scenario: Standard user logs in successfully
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "secret_sauce"
    Then I should be redirected to the inventory page
@Negative
  Scenario: Locked out user cannot log in
    Given I am on the SauceDemo login page
    When I log in with username "locked_out_user" and password "secret_sauce"
    Then I should see an error message containing "locked out"