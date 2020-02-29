# 2020 Sitecore Hackathon - Disaster - Meetup Website documentation

## Summary

The Disaster Meetup site allows the user to create a meetup at a specified location. It also lets the user view the map with the pins for previously created meetups. The site utilizes SXA's maps integration. It's shipped with 4 sample meetups.

## Pre-requisites

 - Sitecore 9.3 with SXA
 - Google Maps API key

## Installation

1. Install the package
2. Publish the site
3. Allow Location Permissions in the browser during the initial load

## Configuration

It works out of the box, no additional configuration needed

## Usage

The homepage serves as the launch point to take the user to the "Create a new meetup" form as well as provide the Google Maps interface with the dropped pins that have meetup information. The user can zoom the map in and out, drag it around, click on dropped pins to read the information pertaining to those meetups. The "Create a new meetup" form is straightfoward, it takes in the information and redirects the user to a success page from which the user can navigate back home or back to the page with the form.

## Video

https://youtu.be/PahMMGKuzJM