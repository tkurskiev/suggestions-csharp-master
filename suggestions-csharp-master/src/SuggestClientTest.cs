﻿using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace suggestionscsharp {

    [TestFixture]
    public class SuggestionsClientTest {

        public SuggestClient api { get; set; }

        [SetUp]
        public void SetUp() {
            var token = Environment.GetEnvironmentVariable("278acbb5adacb6e1c7ba3aad25e802c2c1e9952a");
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            this.api = new SuggestClient(token, url);
        }

        [Test]
        public void SuggestAddressTest() {
            var query = "москва турчанинов 6с2";
            var response = api.QueryAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("119034", address_data.postal_code);
            Assert.AreEqual("7704", address_data.tax_office);
            Assert.AreEqual("Кропоткинская", address_data.metro[0].name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressLocationsKladrTest() {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData();
            location.kladr_id = "65";
            query.locations = new AddressData[] { location };
            var response = api.QueryAddress(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressLocationsFiasCityTest() {
            var query = new AddressSuggestQuery("ватутина");
            var location = new AddressData();
            location.city_fias_id = "44388ad0-06aa-49b0-bbf9-1704629d1d68"; // Южно-Сахалинск
            query.locations = new AddressData[] { location };
            var response = api.QueryAddress(query);
            Assert.AreEqual("693022", response.suggestions[0].data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressBoundsTest() {
            var query = new AddressSuggestQuery("ново");
            query.from_bound = new AddressBound("city");
            query.to_bound = new AddressBound("city");
            var response = api.QueryAddress(query);
            Assert.AreEqual("Новосибирск", response.suggestions[0].data.city);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestAddressHistoryTest() {
            var query = "москва хабар";
            var response = api.QueryAddress(query);
            var address_data = response.suggestions[0].data;
            Assert.AreEqual("ул Черненко", address_data.history_values[0]);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestBankTest() {
            var query = "сбербанк";
            var response = api.QueryBank(query);
            Assert.AreEqual("044525225", response.suggestions[0].data.bic);
            Assert.AreEqual("Москва", response.suggestions[0].data.address.data.city);
            Console.WriteLine(response.suggestions[0].data.opf.type);
            Console.WriteLine(response.suggestions[0].data.state.status);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestBankStatusTest() {
            var query = new BankSuggestQuery("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = api.QueryBank(query);
            Assert.AreEqual("044585398", response.suggestions[0].data.bic);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestBankTypeTest() {
            var query = new BankSuggestQuery("я");
            query.type = new BankType[] { BankType.NKO };
            var response = api.QueryBank(query);
            Assert.AreEqual("044525444", response.suggestions[0].data.bic);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestEmailTest() {
            var query = "anton@m";
            var response = api.QueryEmail(query);
            Assert.AreEqual("anton@mail.ru", response.suggestions[0].value);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestFioTest() {
            var query = "викт";
            var response = api.QueryFio(query);
            Assert.AreEqual("Виктор", response.suggestions[0].data.name);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestFioPartsTest() {
            var query = new FioSuggestQuery("викт");
            query.parts = new FioPart [] { FioPart.SURNAME };
            var response = api.QueryFio(query);
            Assert.AreEqual("Викторова", response.suggestions[0].data.surname);
            Console.WriteLine(string.Join ("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyTest() {
            var query = "7707083893";
            var response = api.QueryParty(query);
            var party = response.suggestions[0];
            var address = response.suggestions[0].data.address;
            Assert.AreEqual("7707083893", party.data.inn);
            Assert.AreEqual("г Москва, ул Вавилова, д 19", address.value);
            Assert.AreEqual("117997 ГОРОД МОСКВА, УЛИЦА ВАВИЛОВА, дом 19", address.data.source);
            Assert.AreEqual("117312", address.data.postal_code);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyStatusTest() {
            var query = new PartySuggestQuery("витас");
            query.status = new PartyStatus[] { PartyStatus.LIQUIDATED };
            var response = api.QueryParty(query);
            Assert.AreEqual("4713008497", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }

        [Test]
        public void SuggestPartyTypeTest() {
            var query = new PartySuggestQuery("витас");
            query.type = PartyType.INDIVIDUAL;
            var response = api.QueryParty(query);
            Assert.AreEqual("773165008890", response.suggestions[0].data.inn);
            Console.WriteLine(string.Join("\n", response.suggestions));
        }
    }
}

