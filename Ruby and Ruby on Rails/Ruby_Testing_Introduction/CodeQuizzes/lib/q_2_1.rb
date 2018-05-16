class Array
  def has_string?
    self.any? {|a| a.instance_of?(String)}
  end
end
