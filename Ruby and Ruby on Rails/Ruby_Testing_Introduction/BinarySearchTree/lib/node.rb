#Jan Bienias
#238201

require 'bigdecimal'

class Node
  attr_reader :value
  attr_accessor :left, :right

  def initialize(value)
    @value = validate(value)
  end

  def insert(new_value)
    new_value = validate(new_value)
    if new_value < @value
      @left.nil? ? @left = Node.new(new_value) : @left.insert(new_value)
    elsif new_value > @value
      @right.nil? ? @right = Node.new(new_value) : @right.insert(new_value)
    else
      raise ArgumentError.new('value is already present in Tree')
    end
  end

  def validate(value)
    if (value.is_a? Integer) || (value.is_a? Float)
      value.to_f
    elsif value.is_a? String
      if numeric?(value)
        value.to_f
      else
        raise ArgumentError.new('value(string) must be numeric')
      end
    end
  end

  def numeric?(str)
    str.match(/\A[+-]?\d+?(_?\d+)*(\.\d+e?\d*)?\Z/) != nil
  end
end
