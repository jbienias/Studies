#Jan Bienias
#238201

require_relative 'node'

class BinarySearchTree
  attr_accessor :root

  def initialize
    @root = nil
  end

  def insert(new_value)
    if @root.nil?
      @root = Node.new(new_value)
    else
      @root.insert(new_value)
    end
  end

  def pre_order(node, array)
    if !node.nil?
      array.push(node.value)
      pre_order(node.left, array)
      pre_order(node.right, array)
    end
  end

  def in_order(node, array)
    if !node.nil?
      in_order(node.left, array)
      array.push(node.value)
      in_order(node.right, array)
    end
  end

  def post_order(node, array)
    if !node.nil?
      post_order(node.left, array)
      post_order(node.right, array)
      array.push(node.value)
    end
  end
end
