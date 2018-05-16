class Array
  def symbolize
    self.map do |f|
      f.to_sym
    end
  end
end
