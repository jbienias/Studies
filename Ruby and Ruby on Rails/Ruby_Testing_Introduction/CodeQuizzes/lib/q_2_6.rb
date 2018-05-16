#https://apidock.com/ruby/Array/reject
#reject Returns a new array containing the items in self for which the block is not true.
class Array
  def super_compact
    self.reject do |e|
      e.nil? || e.empty?
    end
  end
end
