from dronekit import connect
import time
# Connect to the vehicle on a specified port and baud rate
vehicle = connect('com8', baud=115200, wait_ready=True)

# Print the vehicle's basic information
while True:
    print("Vehicle attitude: %s" % vehicle.heading)
    time.sleep(1)

# Close the connection to the vehicle
vehicle.close()