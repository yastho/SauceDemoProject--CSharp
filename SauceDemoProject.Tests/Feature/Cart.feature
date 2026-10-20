Feature: Cart
  As a logged-in user
  I want to add products to my cart
  So that I can buy them later

  Background:
    Given I am on the SauceDemo login page
    When I log in with username "standard_user" and password "secret_sauce"

  Scenario: Add a single item to the cart
    When I add "Sauce Labs Backpack" to the cart
    Then the cart badge should show 1