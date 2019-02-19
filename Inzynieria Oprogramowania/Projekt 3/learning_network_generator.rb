############ Tweeks
input_neurons = [
    "myTank.x",
    "myTank.y",
    "myTank.rotation",
    # "myTank.cannonRotation",
    # "myTank.velocityX",
    # "myTank.velocityY",
    # "myTank.accelerationX",
    # "myTank.accelerationY",
    "myTank.shootCooldown",
    # "myBullet1.x",
    # "myBullet1.y",
    # "myBullet1.velocityX",
    # "myBullet1.velocityY",
    # "myBullet2.x",
    # "myBullet2.y",
    # "myBullet2.velocityX",
    # "myBullet2.velocityY",
    # "myBullet3.x",
    # "myBullet3.y",
    # "myBullet3.velocityX",
    # "myBullet3.velocityY",
    "enemyTank.x",
    "enemyTank.y",
    # "enemyTank.rotation",
    "enemyTank.cannonRotation",
    # "enemyTank.velocityX",
    # "enemyTank.velocityY",
    # "enemyTank.accelerationX",
    # "enemyTank.accelerationY",
    # "enemyTank.shootCooldown",
	"bullet0.x",
    "bullet0.y",
    # "enemyBullet1.velocityX",
    # "enemyBullet1.velocityY",
    "bullet1.x",
    "bullet1.y" #,
    # "enemyBullet2.velocityX",
    # "enemyBullet2.velocityY",
    # "enemyBullet3.x",
    # "enemyBullet3.y",
    # "enemyBullet3.velocityX",
    # "enemyBullet3.velocityY",
    # "currentGameTime"
]

output_neurons = [
    # "enemyTank.controls.turnLeft",
    # "enemyTank.controls.turnRight",
    # "enemyTank.controls.goForward",
    # "enemyTank.controls.goBack",
    # "enemyTank.controls.shoot",
    # "enemyTank.controls.cannonLeft",
    # "enemyTank.controls.cannonRight",
    "myTank.controls.turnLeft",
    "myTank.controls.turnRight",
    "myTank.controls.goForward",
    "myTank.controls.goBack",
    "myTank.controls.shoot",
    "myTank.controls.cannonLeft",
    "myTank.controls.cannonRight"
]

# Sizes of layers
input_layer_size = input_neurons.length
output_layer_size = output_neurons.length
hidden_layer_size = 4

# Arrays generated in R
directory = "C:\/Users\/janbi\/Desktop\/PD-3-TANKS\/"

weights1 = File.read(directory + "data_weight1.txt").gsub("\n", ",").split(",").map(&:to_f)
bias1 = File.read(directory + "data_bias1.txt").gsub("\n", ",").split(",").map(&:to_f)
weights2 = File.read(directory + "data_weight2.txt").gsub("\n", ",").split(",").map(&:to_f)
bias2 = File.read(directory + "data_bias2.txt").gsub("\n", ",").split(",").map(&:to_f)

# Switch for just a dummies
# weights1 = Array.new(input_layer_size * hidden_layer_size) { rand(-4.0 ... 4.0) }
# bias1 = Array.new(hidden_layer_size) { rand(-4.0 ... 4.0) }

# weights2 = Array.new(output_layer_size * hidden_layer_size) { rand(-4.0 ... 4.0) }
# bias2 = Array.new(output_layer_size) { rand(-4.0 ... 4.0) }

# Static arrays
hidden = Array.new(hidden_layer_size) { 0 }
output = Array.new(output_layer_size) { 0 }

############ Some activation functions

def function_sigmoid(str)
    "1 / (1 + Math.pow(Math.E, -" + str + "[i]))"
end

############ Neural net generator

func_str = ""

func_str += "function (e) { \n"
func_str += "var response = {}; \n"
func_str += "myTank = e.data.myTank; \n"
func_str += "enemyTank = e.data.enemyTank; \n"

#####################
func_str += "var bullet0 = {};\n"

func_str += " if(enemyTank.bullets[0]) {
	bullet0.x = enemyTank.bullets[0].x;
	bullet0.y = enemyTank.bullets[0].y;
 } else {
	bullet0.x = 0;
	bullet0.y = 0;
 }"
 
func_str += " var bullet1 = {};\n"

func_str += "if(enemyTank.bullets[1]) {
	bullet1.x = enemyTank.bullets[1].x;
	bullet1.y = enemyTank.bullets[1].y;
 } else {
	bullet1.x = 0;
	bullet1.y = 0;
 }"

######################

func_str += "var input = "
func_str += input_neurons.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var weights1 = "
func_str += weights1.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var bias1 = "
func_str += weights1.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var hidden = "
func_str += hidden.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var i, j; \n"

func_str += "for (i = 0; i < " + hidden_layer_size.to_s + "; i++) { \n"
    func_str += "for (j = 0; j < " + input_layer_size.to_s + "; j++) \n"
        func_str += "hidden[i] += input[j] * weights1[i * " + input_layer_size.to_s + " + j]; \n\n"
    func_str += "hidden[i] = hidden[i] + bias1[i]; \n"
    # Choose activation function from many possible
    func_str += "hidden[i] = " + function_sigmoid("hidden") + "; \n"
    func_str += "} \n";

func_str += "var weights2 = "
func_str += weights2.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var bias2 = "
func_str += bias2.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "var output = "
func_str += output.to_s.gsub("\"", "")
func_str += "; \n"

func_str += "for (i = 0; i < " + output_layer_size.to_s + "; i++) { \n"
    func_str += "for (j = 0; j < " + hidden_layer_size.to_s + "; j++) \n"
        func_str += "output[i] += hidden[j] * weights2[i * " + hidden_layer_size.to_s + " + j]; \n\n"
    func_str += "output[i] = output[i] + bias2[i]; \n"
    # Choose activation function from many possible
    func_str += "output[i] = " + function_sigmoid("output") + "; \n"
    # Rounding???
    func_str += "output[i] = Math.round(output[i]); \n"
    func_str += "} \n";

output_neurons.length.times do |i|
    func_str += output_neurons[i].to_s.gsub("myTank.controls", "response")
    func_str += " = output[" + i.to_s + "]; \n"
end

func_str += "self.postMessage(response); \n"
func_str += "} \n"

############

all_weights = ""
all_weights = bias1.to_s + weights1.to_s + bias2.to_s + weights2.to_s
all_weights = all_weights.gsub("][", ",").gsub("[", "").gsub("]", "").gsub(" ", "")
# Open file to write
out_file_weights = File.new("C:\/Users\/janbi\/Desktop\/PD-3-TANKS\/data_all_weights" + ".csv", "w")
# Save the string
out_file_weights.puts(all_weights)
# Close connection
out_file_weights.close

# Open file to write
out_file = File.new("data_neural_network" + ".txt", "w")
# Save the string
out_file.puts(func_str)
# Close connection
out_file.close