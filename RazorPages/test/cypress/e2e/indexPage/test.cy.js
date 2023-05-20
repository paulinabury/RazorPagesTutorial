/// <reference types="cypress" />
describe("Testing layout of the index page", () => {
    const url = "https://localhost:7120/";

    beforeEach(() => {
        cy.visit(url)
    });

    it('Should cards be visible', () => {
        cy.get('[data-test-id="card_1"]').should('exist');
        cy.get('[data-test-id="card_2"]').should('exist');
        cy.get('[data-test-id="card_3"]').should('exist');
    });

    it('Should be two tabs', () => {
        cy.get('[data-test-id="tab_channels"]').should('exist').click({ force: true });
        cy.get('[data-test-id="tab_videos"]').should('exist').click({ force: true });
    })
});