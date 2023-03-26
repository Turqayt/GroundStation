from dronekit import connect
import time

# Close the connection to the vehicle

class Hesap():
    def __init__(self):
        pass
    def topla(self):
        # Connect to the vehicle on a specified port and baud rate
        vehicle = connect('com3', baud=115200, wait_ready=True)
        # Print the vehicle's basic information
        t = vehicle.heading
        vehicle.cloes()
        return t