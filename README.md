# OpenDesk

#### Open source support ticketing system

[![Build status](https://ci.appveyor.com/api/projects/status/olma9jplhk56psbi?svg=true)](https://ci.appveyor.com/project/spicycoder/open-desk-api)
[![Build Status](https://dev.azure.com/Matrixean-SpicyCoder/open-desk/_apis/build/status/open-desk-CI?branchName=master)](https://dev.azure.com/Matrixean-SpicyCoder/open-desk/_build/latest?definitionId=8&branchName=master)

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/edd6205355f44a88b0cffafbd55d197d)](https://www.codacy.com/manual/spicycoder/open-desk-api?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=spicycoder/open-desk-api&amp;utm_campaign=Badge_Grade)
[![Maintainability](https://api.codeclimate.com/v1/badges/8721ddddcd4b46c64b9a/maintainability)](https://codeclimate.com/github/spicycoder/open-desk-api/maintainability)
[![codecov](https://codecov.io/gh/spicycoder/open-desk-api/branch/master/graph/badge.svg)](https://codecov.io/gh/spicycoder/open-desk-api)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=bugs)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=code_smells)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=coverage)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=ncloc)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=security_rating)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=sqale_index)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=spicycoder_open-desk-api&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)

> Please look at [wiki](https://github.com/spicycoder/open-desk-api/wiki) for more info

---

# Quick Links

- Deployed API

    https://open-desk-api.azurewebsites.net

- Living Documentation

    https://open-desk-docs.azurewebsites.net/

- Resharper Analysis Report

    https://open-desk-renalysis.azurewebsites.net

- Health Checks

    https://open-desk-api.azurewebsites.net/healthchecks-ui

---

# Progress - Functional Requirements

- [x] Create `Ticket`

- [x] Add `Conversation`s to a ticket

    > A `Conversation` should have a minimum of a `Title` and `Content`

- [x] Add `Note`s to a ticket

    > A `Note` is additional information that could be added to a ticket

---

# Progress - NFR

- [x] Build scripts using `Cake`
- [x] Code Coverage using [CodeCov](https://app.netlify.com/teams/spicycoder/sites) & [SonarQube](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api)
- [x] Code Quality using `Resharper CLT`, [SonarQube](https://sonarcloud.io/dashboard?id=spicycoder_open-desk-api), [Codacy](https://www.codacy.com/manual/spicycoder/open-desk-api?utm_source=github.com&utm_medium=referral&utm_content=spicycoder/open-desk-api&utm_campaign=Badge_Grade) and [CodeClimate](https://codeclimate.com/github/spicycoder/open-desk-api/maintainability)
- [x] CI using [AppVeyor](https://ci.appveyor.com/project/spicycoder/open-desk-api)
- [x] BDD using `SpecFlow`, `InMemory Testing`, Report Generation using `Pickle`, Code Coverage using `Coverlet`
- [x] Deploy [Resharper Code Analysis report](https://open-desk-renalysis.azurewebsites.net) & [Living Documentation](https://open-desk-docs.azurewebsites.net/) online
- [x] [Health Checks](https://open-desk-api.azurewebsites.net/healthchecks-ui)
- [ ] MiniProfiler
- [ ] Create Docker image on every build (CI)

---